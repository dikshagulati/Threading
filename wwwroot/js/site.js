// Write your JavaScript code.
$(document).ready(function () {
    $(".button").click(function (sender) {
        var options = {};
        options.url = "/home/test";
        options.type = "Get";
        //var obj = Employee;
        //obj.name = $("#name").val();
        //obj.gender = $("#gender").val();
        //obj.salary = $("#salary").val();
        //console.dir(obj);
        //options.data = JSON.stringify(obj);
        options.contentType = "application/json";
        options.dataType = "html";

        options.success = function (data) {
            $(".placeholder").html(data);
        },

        options.error = function (data) {
            $(".placeholder").html(data.responseText);
            };
        $.ajax(options);
    });

    $(".buttonUpdate").click(function (sender) {
        var options = {};
        options.url = "/home/TestData";
        options.type = "Get";
        //var obj = Employee;
        //obj.name = $("#name").val();
        //obj.gender = $("#gender").val();
        //obj.salary = $("#salary").val();
        //console.dir(obj);
        //options.data = JSON.stringify(obj);
        options.contentType = "application/json";
        options.dataType = "html";

        options.success = function (response, statusText, jqXhr) {
            var data = jQuery.parseJSON(response);
            $(".placeholder .test1").html(data.Test1);
        },
            options.error = function () {
                $(".placeholder.test1").html("Error while calling the Web API!");
            };
        $.ajax(options);
    });

    $(".fasDetails").click(function (sender) {
        var options = {};
        options.url = "/home/notifyUser";
        options.type = "Get";
        options.contentType = "application/json";
        options.dataType = "html";

        options.success = function (response, statusText, jqXhr) {
            var data = jQuery.parseJSON(response);
            $(".placeholder .test1").html(data.Test1);
        },
            options.error = function () {
                $(".placeholder.test1").html("Error while calling the Web API!");
            };
        $.ajax(options);
    });


    $(".noteSave").click(function (sender) {
        var options = {};
        options.url = "/home/saveNote";
        options.type = "Get";
        options.contentType = "application/json";
        options.dataType = "html";

        options.success = function (response, statusText, jqXhr) {
            var data = jQuery.parseJSON(response);
            $(".placeholder .test1").html(data.Test1);
        },
            options.error = function () {
                $(".placeholder.test1").html("Error while calling the Web API!");
            };
        $.ajax(options);
    });

});