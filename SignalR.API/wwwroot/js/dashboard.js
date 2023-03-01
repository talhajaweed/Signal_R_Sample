"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

$(function () {
    connection.start().then(function () {
		alert('Connected to dashboardHub');
		//InvokeProducts();
		//// InvokeSales();
		//// InvokeCustomers();
    }).catch(function (err) {
        return console.error(err.toString());
    });
});

//Product
function InvokeProducts() {
	connection.invoke("SendProducts").catch(function (err) {
		return console.error(err.toString());
	});
}

connection.on("ReceivedProducts", function (products) {
	console.log(products);
	BindProductsToGrid(products);
});

function BindProductsToGrid(products) {
	$('#tblProduct tbody').empty();

	var tr;
	$.each(products, function (index, product) {
		tr = $('<tr/>');
		tr.append(`<td>${product.productAirID}</td>`);
		tr.append(`<td>${product.productAirName}</td>`);
		tr.append(`<td>${product.createdBy}</td>`);
		tr.append(`<td>${product.price}</td>`);
		$('#tblProduct').append(tr);
	});
}


//

function SaveSession() {
    var inputVal = document.getElementById("myInput").value;
    connection.invoke("SaveSession", inputVal).catch(function (err) {
        return console.error(err.toString());
    })
}

function showAlert() {
	var inputVal = document.getElementById("myInput").value;
	//alert("You entered from js: " + inputVal);

	const data = {
		createdBy: "",
		sessionID: ""
	};

	data.createdBy = inputVal;

	fetch('http://localhost:5122/api/Login/SaveSessioin', {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(data)
	})
		.then(response => {
			if (!response.ok) {
				throw new Error('Network response was not ok');
			}
			//console.log('respnse', response.json());
		})
		.then(data => {
			console.log(data);
		})
		.catch(error => {
			console.error('Error:', error);
		});
	  

	// fetch('http://localhost:5122/api/Login/' + inputVal + '/123')
	// .then(response => {
	// 	if (!response.ok) {
	// 	throw new Error('Network response was not ok');
	// 	}
	// 	console.log(response.json());
	// })
	// .then(data => {
	// 	console.log(data);
	// })
	// .catch(error => {
	// 	console.error('Error:', error);
	// });

}