function viewContactSplitStatus(SplitID) {
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


//#region events

$("#ParticipantSelectionDropdown").on("change", () => {
	AddUserToParticipantList();
})

//endregion