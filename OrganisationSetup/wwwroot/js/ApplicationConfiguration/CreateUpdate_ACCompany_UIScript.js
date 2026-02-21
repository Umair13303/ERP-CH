/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";

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
        if (validater()) {
            e.preventDefault();
            createUpdateDataIntoDB();
        }
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

    var jsonData = {
        OperationType: operationType,
        GuID: guID ? guID : null,
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
        data: JSON.stringify(jsonData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            initLoading();
        },
        success: function (response) {
            if (response.IsSuccess == true) {
                toastr.success(response.message);
                $("#ACCompanyForm").removeClass('was-validated');
            }
            else {
                toastr.info(response.message);
            }
        },
        error: function (xhr) {
            toastr.error("System Error: " + xhr.statusText);
        },
        complete: function () {
            stopLoading();
            clearInputFields();
        }
    });
}
function clearInputFields() {
    $(".form-control").val('');
    $(".select2").val('-1').trigger("change");

}
$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});