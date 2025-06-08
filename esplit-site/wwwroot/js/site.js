// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const { get } = require("jquery");

// Write your JavaScript code.
function viewContactStatus(splitid) {
	$.ajax({
		url: "/Dashboard/SplitParticipants?splitid=" + splitid,
		type: "GET",
		success: function (response) {
			document.getElementById("SplitParticipantsContainer").innerHTML = response;
		},
		error: function (xhr, status, error) {
			console.log('Error:', error);
		}
	});
}