const Split = {

	properties: {
		// this one always stays constant 
		allContacts: [],

		// this one we update it every time we input something in searchbox
		// this is the actual list we show in the drop down
		filteredContacts: [],

		// this is the list where we will be getting all the values from, {id and amount to be precise}
		participantList: []
	},

	methods: {
		viewSplitStatus: function (SplitID) {
			$.ajax({
				url: "/Dashboard/SplitParticipants?SplitID=" + SplitID,
				type: "GET",
				success: function (response) {
					document.getElementById("SplitParticipantsContainer").innerHTML = response;

					let ParticipantsModal = new bootstrap.Modal(document.getElementById('SplitParticipantsModal'));
					ParticipantsModal.show();

				},
				error: function (xhr, SplitStatus, error) {
					console.log('Error:', error);
				}
			});
		},

		addSplitPopup: async function () {
			let SplitModal = new bootstrap.Modal(document.getElementById("AddSplitModal"));
			SplitModal.show();

			//we have to initially populate the list
			this.allContacts = await Contact.getContacts();
			//further operations we carry out via event listeners
		},

		splitToggleActionPerformed: function (SplitID, SplitStatus) {
			if (SplitStatus == 11) {
				Split.methods.closeSplit(SplitID);
			} else if (SplitStatus == 1) {
				Split.methods.approveSplit(SplitID);
			} else if (SplitStatus == 3) {
				Split.methods.paySplit(SplitID);
			}
		},

		closeSplit: function (SplitID) {
			console.log("close split " + SplitID)
			$.ajax({
				url: "/Dashboard/CloseSplit?SplitID=" + SplitID,
				type: "DELETE",
				success: function (response) {

				},
				error: function (error) {

				}
			});
		},

		approveSplit: function (SplitID) {
			console.log("approve split " + SplitID)
			$.ajax({
				url: "/Dashboard/ApproveSplit?SplitID=" + SplitID,
				type: "PATCH",
				success: function (response) {

				},
				error: function (error) {

				}
			});
		},

		rejectSplit: function (SplitID) {
			console.log("reject split" + SplitID)
			$.ajax({
				url: "/Dashboard/RejectSplit?SplitID=" + SplitID,
				type: "DELETE",
				success: function (response) {

				},
				error: function (error) {

				}
			});
		},

		paySplit: function (SplitID) {
			console.log("pay split" + SplitID)
			$.ajax({
				url: "/Dashboard/PayDues?SplitID=" + SplitID,
				type: "",
				success: function (response) {

				},
				error: function (error) {

				}
			});
		},

		// This function is called when the user types in the search box, just to filter based on search input
		updateAndBindFilteredContacts: function (searchTerm) {
			this.filteredContacts = this.allContacts.filter(contact => {
				return contact.userData.fullName.toLowerCase().includes(searchTerm.toLowerCase()) ||
					contact.userData.userName.toLowerCase().includes(searchTerm.toLowerCase());
			});

			// Bind the filtered contacts to the dropdown in the ui
			Split.methods.bindFilteredContactsToDDL();
		},

		// filtered list is then bound to the dropdown in ui
		bindFilteredContactsToDDL: function () {
			let dropdown = document.getElementById("ContactSelectionDropdown");
			dropdown.innerHTML = "";
			this.filteredContacts.forEach(contact => {
				let item = document.createElement("li");
				item.dataset.id = `FContactID${contact.userData.userID}`;
				item.innerHTML = `
				<div class="d-flex align-items-center gap-2">
					<button type="button" class="btn btn-sm btn-success addPButton" onclick="Split.addToParticipantList(${contact.userData.userID})">&plus;</button>
					<span>${contact.userData.fullName} <span class="text-muted">(${contact.userData.userName})</span></span>
				</div>
			`;

				dropdown.appendChild(item);
			});
			if (this.filteredContacts.length === 0) {
				let item = document.createElement("li");
				item.value = "";
				item.innerHTML = "No contacts found";
				dropdown.appendChild(item);
			}
		},

		// This function is called when the user clicks on the add button in the dropdown
		addToParticipantList: function (contactID) {
			let contact = Split.properties.allContacts.find(c => c.userData.userID === contactID);
			Split.properties.participantList.push(contact);
			Split.methods.bindParticipantsList();
			document.getElementById("ContactSelectionDropdown").innerHTML = "";
		},

		// This function is called when the user clicks on the remove button in the participant list
		removeFromParticipantList: function (contactID) {
			let index = Split.properties.participantList.findIndex(c => c.userData.userID === contactID);
			if (index !== -1) {
				Split.properties.participantList.splice(index, 1);
			}
			let dataId = `FContactID${contactID}`;
			let participantEntry = document.querySelector(dataId);
			if (participantEntry) {
				participantEntry.remove();
			}
			Split.methods.bindParticipantsList();
		},

		// This function binds the participant list to the UI, this generally happens after adding or removing a participant
		bindParticipantsList: function () {
			let listToPopulate = document.getElementById("ParticipantsList");
			listToPopulate.innerHTML = "";
			Split.properties.participantList.forEach(contact => {
				let listItem = document.createElement("li");
				listItem.setAttribute("data-participant-id", contact.userData.userID);
				listItem.innerHTML = `
				<div class="d-flex align-items-center gap-2 flex-wrap">
					<span class="fw-bold">${contact.userData.fullName}</span>
					<span class="text-muted">(${contact.userData.userName})</span>
					<input type="number" data-calc-id="Participant${contact.userData.userID}" class="form-control form-control-sm text-muted" style="width: 80px;" value="0" readonly/>
					<input type="number" data-amount-id="Participant${contact.userData.userID}" class="form-control form-control-sm" style="width: 80px;" value="0"/>
					<button class="btn btn-sm btn-danger" onclick="Split.removeFromParticipantList(${contact.userData.userID})">&minus;</button>
				</div>
			`;

			listToPopulate.appendChild(listItem);
			});
		},


		prepareSplitData: function () {
			let splitAmount = document.getElementById("SplitAmount").value;
			let splitDescription = document.getElementById("SplitDescription").value;
			if (splitDescription === "" || splitAmount === "" || Split.properties.participantList.length === 0) {
				Common.showErrorModal("Please fill all the fields and add participants.");
				return null;
			}
			let participants = Split.properties.participantList.map(contact => {
				return {
					splitParticipantID: contact.userData.userID,
					oweAmount: parseFloat(document.querySelector(`[data-amount-id="Participant${contact.userData.userID}"]`).value)
				};
			});
			return {
				info: {
					splitAmount: parseFloat(splitAmount),
					splitDescription: splitDescription,
				},
				contacts: participants
			};
		},

		addSplit: function () {
			let splitData = Split.methods.prepareSplitData();
			if (!splitData) return;
			$.ajax({
				url: "/Dashboard/AddSplit",
				type: "POST",
				contentType: "application/json",
				data: JSON.stringify(splitData),
				success: function (response) {
					if (response.success) {
						//let splitContainer = document.getElementById("SplitContainer");
						//splitContainer.innerHTML += response.splitHtml;
						let addSplitModal = bootstrap.Modal.getInstance(document.getElementById("AddSplitModal"));
						addSplitModal.hide();
					} else {
						Common.showErrorModal(response.message);
					}
				},
				error: function (xhr) {
					Common.showErrorModal(xhr.responseText);
				}
			});
		}
	}

};

// Initialize the Split object and populate all contacts on page load
$('#ContactSelectionSearch').on('input', function () {
	let searchTerm = $(this).val();
	if (searchTerm.length >= 3) {
		Split.methods.updateAndBindFilteredContacts(searchTerm);
	} if (searchTerm.length === 0) {
		document.getElementById("ContactSelectionDropdown").innerHTML = "";
	}
});

$('.addPButton').on('click', function () {
	document.getElementById("ContactSelectionDropdown").innerHTML = "";
});

// handling split logic here for split type change
$('AmountDistributionType').on("click", function () {
	switch (this.val()) {
		case 'P':
			this.val('E');
			Split.

			break;
		case 'E':


			break;
		case 'A':

			break;
	}
});
	// handle the submittion of the split
	$('#SplitSubmitButton').on('click', function () {
		Split.methods..addSplit();
	});

const Contact = {

	populateAllContacts: function () {
		Split.properties.allContacts = getContacts();
	},

	getContacts: function () {
		return new Promise(function (resolve, reject) {
			$.ajax({
				url: "Contacts/GetContacts",
				type: "GET",
				dataType: "json",
				success: function (response) {
					resolve(response);
				},
				error: function (xhr) {
					Common.showErrorModal(xhr.responseText);
					reject(xhr);
				}
			});
		});
	},

	sendContactRequest: function () {
		let toUserName = $("#toUserName").val();
		console.log("contact req" + toUserName)
		if (toUserName != "") {
			$.ajax({
				url: "/Contacts/SendRequest?toUserName=" + toUserName,
				type: "POST",
				success: function (response) {
					$("#SentContactRequestContainer").append(response);
				},
				error: function (xhr) {
					let err = JSON.parse(xhr.responseText);
					if (err.success === false) {
						Common.showErrorModal(err.message);
					}
				}
			});
		} else {
			Common.showErrorModal("bro fill the input field")
		}
	},

	approveContactRequest: function (ContactID) {
		$.ajax({
			url: "/Contacts/ApproveRequest?ContactID=" + ContactID,
			type: "PATCH",
			success: function (response) {
				if (response) {
					let CID = `#Contact-${ContactID}`;
					$(CID).remove();
					console.log(response);
					$("#MyContactsContainer").append(response);
				}
			},
			error: function (xhr) {
				let err = JSON.parse(xhr.responseText);
				if (err.success === false) {
					Common.showErrorModal(err.message);
				}
			}
		});
	},

	rejectContactRequest: function (ContactID) {
		$.ajax({
			url: "/Contacts/RejectRequest?ContactID=" + ContactID,
			type: "PATCH",
			success: function (response) {
				if (response.success == true) {
					let CID = `#Contact-${ContactID}`;
					$(CID).remove();
				} else {
					Common.showErrorModal(response.message);
				}
			},
			error: function (xhr) {
				Common.showErrorModal(xhr.responseText.message);
			}
		});
	},

	deleteContact: function (ContactID, type) {
		$.ajax({
			url: "/Contacts/DeleteRequest?ContactID=" + ContactID + "&type=" + type,
			type: "DELETE",

			success: function (response) {
				if (response.success == true) {
					let CID = `#Contact-${ContactID}`;
					$(CID).remove();
				} else {
					Common.showErrorModal(response.message);
				}
			},
			error: function (xhr) {
				Common.showErrorModal(xhr.responseText.message);
			}
		});
	}
}


const Common = {
	showErrorModal: function (message) {
		document.getElementById('errorModalBody').innerText = message;
		const modal = new bootstrap.Modal(document.getElementById('errorModal'));
		modal.show();
	},

	rePopulateContacts: function () {
		let listToPopulate = document.getElementById("ParticipantSelectionListed");
		
	}
}


const Notify = {
	deleteNotification: function (NotificationID) {
		$.ajax({
			url: "/Notifications?NotificationID=" + NotificationID,
			type: "DELETE",
			success: function (response) {
				if (response.success = true) {
					let NID = `#Notification-${NotificationID}`;
					$(NID).remove();
				} else {
					Common.showErrorModal(response.message);
				}
			},
			error: function (error) {
				Common.showErrorModal(error);
			}
		});
	}
}
