/// <reference path="jquery-1.7.1-vsdoc.js"/>

var Controller = "Home";
var Action = "Index";
var Main = $("div#main");
var Form = $("form");
var ValidationOptions = {
    ignore: '.ignore',
    debug: 'true'
};

$(function () {
    Main = $("div#main");
});

function composeUrl(action, controller) {
    if (null == action) {
        return Controller + "/" + Action;
    }
    else if (null == controller) {
        return Controller + "/" + action;
    }
    else {
        return controller + "/" + action;
    }
}

function go(controller, action, id) {
    if (null == id) {
        if (null == action) {
            Main.load(controller);
        }
        else {
            Main.load(controller + "/" + action);
        }
    }
    else {
        Main.load(controller + "/" + action + "/" + id);
    }
}

function submitForm(action, controller) {
    action = null != action ? action : "Save";
    //Form.validate(ValidationOptions)
    if (true) {
        $.post(composeUrl(action, controller), Form.serialize(),
            function (response) {
                Main.html(response);
            });
    }
}

function countDish(checked, price) {
    var sum = $(".sum");
    if(checked) {
        sum.text(parseInt(sum.text()) + price);
    }
    else {
        sum.text(parseInt(sum.text()) - price);
    }
}

//function ChangeVisibility(id, isVisible) {
//    $.get(composeUrl("ChangeVisibility"), { id: id, isVisible: isVisible },
//        function (response) {
//            var button = $("*[id=" + response.id + "] div div#visibility")
//        });
//    }



function hideAllMenu() {
    return;
    $("table.dish-menu tr[class!=category]").each(function (i, item) {
        item = $(item);
        item.css('visibility', 'hidden');
    });
}

function showCategory(categoryName) {
    
}

function NotImplemented(objName) {
    alert(objName.toString() + " not implemented");
}