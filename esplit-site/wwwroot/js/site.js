const Split = {

	allContacts: [],
	filteredContacts: [],

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

	addSplit: function () {
		let SplitModal = new bootstrap.Modal(document.getElementById("AddSplitModal"));
		SplitModal.show();
	},

	addUserToParticipantList: function () {
		let selection = $("#ParticipantSelectionDropdown option:selected");

		let template = `<div class="list-group-item d-flex justify-content-between align-items-center Participant-Entry" id="Participant${selection.val()}">
                        <div>
                            <span>${selection.text()}</span>
                            <input type="number" class="form-control d-inline-block ms-2" style="width: 100px;" />
                            [<input type="number" class="text-muted amount" id="AmountOf${selection.val()}" readonly />]
                        </div>
                        <button type="button" class="btn btn-sm btn-danger" onclick="RemoveParticipant(${selection.val()})">&times;</button>
                    </div>`;

		let element = document.createElement("div");
		element.innerHTML = template;
		document.getElementById("ParticipantsList").appendChild(element);
	},

	removeParticipant: function (ParticipantID) {
		let ParticipantToRemove = `#Participant${ParticipantID}`;
		$(ParticipantToRemove).remove();
	},

	splitToggleActionPerformed: function (SplitID, SplitStatus) {
		if (SplitStatus == 11) {
			this.closeSplit(SplitID);
		} else if (SplitStatus == 1) {
			this.approveSplit(SplitID);
		} else if (SplitStatus == 3) {
			this.paySplit(SplitID);
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
	}

};

const Contact = {

	populateAllContacts: function () {
		Split.allContacts = getContacts();
	},

	getContacts: function () {
		$.ajax({
			url: "Contacts/GetContacts",
			type: "GET",
			success: function (response) {
				return response;
			},
			error: function (xhr){
				Common.showErrorModal(xhr.responseText);
				return [];
			}
		})
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

//#region events

$("#ParticipantSelectionDropdown").on("change", () => {
	Split.addUserToParticipantList();
})

//endregion

function viewSplitStatus(SplitID) {
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
}

function AddNewSplit() {
	let SplitModal = new bootstrap.Modal(document.getElementById("AddSplitModal"));
	SplitModal.show();
}

function AddUserToParticipantList() {
	let selection = $("#ParticipantSelectionDropdown option:selected");

	let template = `<div class="list-group-item d-flex justify-content-between align-items-center Participant-Entry" id="Participant${selection.val()}">
                        <div>
                            <span>${selection.text()}</span>
                            <input type="number" class="form-control d-inline-block ms-2" style="width: 100px;" />
                            [<input type="number" class="text-muted amount" id="AmountOf${selection.val()}" readonly />]
                        </div>
                        <button type="button" class="btn btn-sm btn-danger" onclick="RemoveParticipant(${selection.val()})">&times;</button>
                    </div>`;

	let element = document.createElement("div");
	element.innerHTML = template;
	document.getElementById("ParticipantsList").appendChild(element);

}

function RemoveParticipant(ParticipantID) {
	let ParticipantToRemove = `#Participant${ParticipantID}`;
	$(ParticipantToRemove).remove();
}

function splitToggleActionPerformed(SplitID, SplitStatus) {
	if (SplitStatus == 11) {
		closeSplit(SplitID);
	} else if (SplitStatus == 1) {
		approveSplit(SplitID);
	} else if (SplitStatus == 3) {
		paySplit(SplitID);
	}
}

function closeSplit(SplitID) {
	console.log("close split " + SplitID)
	$.ajax({
		url: "/Dashboard/CloseSplit?SplitID=" + SplitID,
		type: "DELETE",
		success: function (response) {
			
		},
		error: function (error) {

		}
	});
}

function approveSplit(SplitID) {
	console.log("approve split " + SplitID)
	$.ajax({
		url: "/Dashboard/ApproveSplit?SplitID=" + SplitID,
		type: "PATCH",
		success: function (response) {

		},
		error: function (error) {

		}
	});
}
function rejectSplit(SplitID) {
	console.log("reject split" + SplitID)
	$.ajax({
		url: "/Dashboard/RejectSplit?SplitID=" + SplitID,
		type: "DELETE",
		success: function (response) {

		},
		error: function (error) {

		}
	});
}

function paySplit(SplitID) {
	console.log("pay split" + SplitID)
	$.ajax({
		url: "/Dashboard/PayDues?SplitID=" + SplitID,
		type: "",
		success: function (response) {

		},
		error: function (error) {

		}
	});
}

function deleteNotification(NotificationID) {
	$.ajax({
		url: "/Notifications?NotificationID=" + NotificationID,
		type: "DELETE",
		success: function (response) {
			if (response.success = true) {
				let NID = `#Notification-${NotificationID}`;
				$(NID).remove();
			} else {
				showErrorModal(response.message);
			}
		},
		error: function (error) {
			showErrorModal(error);
		}
	});
}

function SendContactRequest() {
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
					showErrorModal(err.message);
				}
			}
		});
	} else {
		showErrorModal("bro fill the input field")
	}
}

function ApproveContactRequest(ContactID) {
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
				showErrorModal(err.message);
			}
		}
	});
}
function RejectContactRequest(ContactID) {
	$.ajax({
		url: "/Contacts/RejectRequest?ContactID=" + ContactID,
		type: "PATCH",
		success: function (response) {
			if (response.success == true) {
				let CID = `#Contact-${ContactID}`;
				$(CID).remove();
			} else {
				showErrorModal(response.message);
			}
		},
		error: function (xhr) {
			showErrorModal(xhr.responseText.message);
		}
	});
}
function DeleteContact(ContactID, type) {
	$.ajax({
		url: "/Contacts/DeleteRequest?ContactID=" + ContactID + "&type=" + type,
		type: "DELETE",

		success: function (response) {
			if (response.success == true) {
				let CID = `#Contact-${ContactID}`;
				$(CID).remove();
			} else {
				showErrorModal(response.message);
			}
		},
		error: function (xhr) {
			showErrorModal(xhr.responseText.message);
		}
	});
}
 
function showErrorModal(message) {
	document.getElementById('errorModalBody').innerText = message;
	const modal = new bootstrap.Modal(document.getElementById('errorModal'));
	modal.show();
}

<li>
	<input type="hidden" value="${contact.userid}"></input>
	<button class="btn btn-primary" onclick="addToParticipantList(${contact.userid})">+</button>
	<span class="contact-name">${contact.fullname}</span>
	<span class="contact-name">${contact.username}</span>
	<input type="number" data-calc-amount="" class="text-muted amount " readonly />
	<input type="number" data-raw-amount="" class="form-control d-inline-block ms-2" id="AmountOfContactID"/>"

</li>