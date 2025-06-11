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
		success: function (resopnse) {
			
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
		success: function (resopnse) {

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
		success: function (resopnse) {

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
		success: function (resopnse) {

		},
		error: function (error) {

		}
	});
}

function deleteNotification(NotificationID) {
	console.log("delete notification " + NotificationID)
	$.ajax({
		url: "/Notifications?NotificationID=" + NotificationID,
		type: "DELETE",
		success: function (resopnse) {

		},
		error: function (error) {

		}
	});
}

function SendContactRequest() {
	let toUserName = $("#toUserName").Value
	console.log("contact req" + toUserName)
	if (toUserName != "") {
		$.ajax({
			url: "/Contacts/AddContact?toUserName=" + toUserName,
			type: "POST",
			success: function (resopnse) {

			},
			error: function (error) {

			}
		});
	} else {
		console.log("bro fill the input field")
	}
}

function AddUserToParticipantList() {
	let selection = $("#ParticipantSelectionDropdown option:selected");

	let template = `<div class="list-group-item d-flex justify-content-between align-items-center Participant-Entry" id="Participant${selection.val()}">
                        <div>
                            <span>${selection.text()}</span>
                            <input type="number" class="form-control d-inline-block ms-2" style="width: 100px;" />
                            [<span class="text-muted amount" id="AmountOf${selection.val()}">actual amount</span>]
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

//#region events

$("#ParticipantSelectionDropdown").on("change", () => {
	AddUserToParticipantList();
})

//endregion