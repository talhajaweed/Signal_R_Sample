"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

$(function () {
    connection.start().then(function () {
		alert('Connected to dashboardHub');

		InvokeProducts();
		// InvokeSales();
		// InvokeCustomers();

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