/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='- 1'>Select an option</option>";


/* ------ Depending DDL's ------ */
function getCountryList() {
    $.ajax({
        url: window.basePath + "ApplicationConfiguration/ACCompanyManagement/populateCountryListByParam",
        type: "GET",
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListCountry").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListCountry").append(new Option(item.text || item.description, item.id || item.value));
            });
        },
        complete: function () {

        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
function getCityList() {
    var countryId = $("#DropDownListCountry :selected").val();
    if (!countryId || countryId == "-1") {
        $("#DropDownListCity").empty().append(dropDownListInitOption);
        return;
    }
    $.ajax({
        url: window.basePath + "ApplicationConfiguration/ACCompanyManagement/populateCityListByParam",
        type: "GET",
        data: { countryId: countryId },
        dataType: "json",
        beforeSend: function () {

        },
        success: function (data) {
            $("#DropDownListCity").empty().append(dropDownListInitOption);
            $.each(data, function (index, item) {
                $("#DropDownListCity").append(new Option(item.text || item.description, item.id || item.value));
            });
        },
        complete: function () {

        },
        error: function (error) {
            console.error("Error: " + error);
        }
    });
}

/* ------ Change Cases DDL's ------ */
function changeEventHandler() {
    $("#DropDownListCountry").on("change", function () {
        var countryId = $(this).val();
        alert(countryId)
        getCityList();
    });
    $("#ButtonSaveData, #ButtonUpdateData").on("click", function (e) {
        e.preventDefault();
        createUpdateDataIntoDB();
    });
}

/* ------ Call Initial Components ------ */
function initialize() {
    getCountryList();
    const intputMasking = new UIMasking();
    intputMasking.initialize();
    $('.select2').select2({
        theme: 'bootstrap-5',
        width: '100%'
    });
    changeEventHandler();
}
/* ------ Validation for user input ------ */
function validater() {
    var form = document.getElementById("ACCompanyForm");
    if (!form.checkValidity()) {
        form.classList.add('was-validated');

        var $firstInvalid = $(form).find(":invalid").first();
        if ($firstInvalid.length) {
            $firstInvalid.trigger("focus");
        }

        toastr.warning("Please fill in all required fields correctly.");
        return false;
    }
    return true;
}

/* ------ Add/Edit/Delete Operation ------ */
function createUpdateDataIntoDB() {
    var operationType = $("#OperationType").val();
    var guID = $("#GuID").val();
    var description = $("#TextBoxDescription").val();
    var countryId = $("#DropDownListCountry :selected").val();
    var cityId = $("#DropDownListCity :selected").val();
    var contact = $("#TextBoxContact").val();
    var email = $("#TextBoxEmail").val();
    var website = $("#TextBoxWebsite").val();
    var address = $("#TextAreaAddress").val();

    var postedData = {
        OperationType: operationType,
        GuID: guID,
        Description: description,
        CountryId: countryId,
        CityId: cityId,
        Contact: contact,
        Email: email,
        Website: website,
        Address: address
    };
    $.ajax({
        url: window.basePath + "ApplicationConfiguration/ACCompanyManagement/createUpdateCompany",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(postedData),
        beforeSend: function () {
            initLoading();
        },
        success: function (result) {
            if (result.isSuccess || result.IsSuccess) {
                toastr.success(result.message || "Record processed successfully!");
                $("#ACCompanyForm").removeClass('was-validated');
            } else {
                toastr.error(result.message || "Failed to save data.");
            }
        },
        error: function (xhr) {
            toastr.error("System Error: " + xhr.statusText);
        },
        complete: function () {
            stopLoading();
        }
    });
}

$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});