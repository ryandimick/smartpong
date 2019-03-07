//*********** TEMPLATE POP UP MESSAGES ***************
function ShowAscernSuccess(message) {
    Notify(message, 'bottom-right', '5000', 'success', 'fa-thumbs-up', true);
}

function ShowAscernError(message) {
    Notify(message, 'bottom-right', '5000', 'danger', 'fa-thumbs-down', true);
}

function ShowAscernSuccess(message, length) {
    Notify(message, 'bottom-right', length, 'success', 'fa-thumbs-up', true);
}

function ShowAscernError(message, length) {
    Notify(message, 'bottom-right', length, 'danger', 'fa-thumbs-down', true);
}
//****************************************************

//************ LINKED LIST MOVE ITEMS ******************
function moveItems(origin, dest) {
    $(origin).find(':selected').appendTo(dest);
}

function moveAllItems(origin, dest) {
    $(origin).children().appendTo(dest);
}

function mergeObjects(obj1, obj2) {
    var obj3 = {};

    for (var attrname in obj1) { obj3[attrname] = obj1[attrname]; }
    for (var attrname in obj2) { obj3[attrname] = obj2[attrname]; }

    return obj3;
}
//********************************************************

//**************** KENDO JAVASCRIPT WINDOW ******************************
function openKendoWindow(url, title, maxHeight, maxWidth, additionalOptions) {
    var content, options;

    // Default options
    options = {
        title: title,
        maxHeight: maxHeight,
        maxWidth: maxWidth,
        content: url,
        close: onKWindowClose
    };
    // Merge options
    options = mergeObjects(options, additionalOptions);

    // Add the div to initialize to the DOM
    var container = $('<div />').attr("id", "kWindowDiv").appendTo(document.body);

    // Initialize Kendo Window object
    var wnd = container.kendoWindow(options).data('kendoWindow');

    //add method to close dynamically created window attatched to button in window content
    $("#btnSaveRoleRecord").on("click", function () { wnd.close(); });
}

function onKWindowClose() {
    var elem = document.getElementById("kWindowDiv");
    elem.parentNode.removeChild(elem);
}

function offSetKendoWindow(top, left) {
    $("#kWindowDiv").closest(".k-window").css({
        top: top,
        left: left
    });
}

function offSetKendoWindow(name, top, left) {
    $(name).closest(".k-window").css({
        top: top,
        left: left
    });
}
//*******************************************************************

//************ KENOD GRID CUSTOMIZATION ****************************
function HideControl(fieldName, e) { //GRID EDITOR HIDE CONTROL
    var cont = $(e.container);
    HideFieldLabel(cont.find("label[for='" + fieldName + "']"));
    HideFieldField(cont.find("#" + fieldName));
}

function HideFieldLabel(control) { //GRID EDITOR HIDE LABEL
    control.parent(".editor-label").hide();
}

function HideFieldField(control) { //GRID EDITOR HIDE FIELD
    control.parent(".editor-field").hide();
}

function HideAllRowButtons(kGrid, btnName) { //HIDE GRID BUTTON
    var kFind = ".k-grid-" + btnName;
    var grid = $(kGrid).data("kendoGrid");
    var gridData = grid.dataSource.view();
    for (var i = 0; i < gridData.length; i++) {
        var currentUid = gridData[i].uid;
        var currentRow = grid.table.find("tr[data-uid='" + currentUid + "']");
        var kButton = $(currentRow).find(kFind);
        kButton.hide();
    }
}

function ShowGridButton(currentRow, btnName) { //SHOW CURRENT ROW BUTTON
    var kFind = ".k-grid-" + btnName;
    var kButton = $(currentRow).find(kFind);
    kButton.show();
}

function HideGridButton(currentRow, btnName) { //HIDE CURRENT ROW BUTTON
    var kFind = ".k-grid-" + btnName;
    var kButton = $(currentRow).find(kFind);
    kButton.hide();
}
//***************************************************************

//function LoadPartialViewInDiv(divId, controller, action) {
//    var dUrl1 = "@(Html.Raw(Url.Action(\"" + action + "\"";
//    var dUrl2 = "\"" + controller + "\")))";
//    var dUrl = dUrl1.concat(",").concat(dUrl2);
//    $(divId).load(dUrl);
//}

function imageHover(dom, action) {
    if (action === "in") {
        $(dom).find("[col=g]").css("display", "none");
        $(dom).find("[col=b]").css("display", "inline-block");
    }
    else {
        $(dom).find("[col=b]").css("display", "none");
        $(dom).find("[col=g]").css("display", "inline-block");
    }
}

function displayLoading(target, show) {
    var element = $(target);
    kendo.ui.progress(element, show);
    //setTimeout(function(){
    //    kendo.ui.progress(element, false);
    //}, 2000);        
}