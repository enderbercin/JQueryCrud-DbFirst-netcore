var number = 0;
var okulTurList;

$(function () {
    GetOkulTurleri();
    $(document).on('hide.bs.modal', '#form-modal', function () {
        number = 0;
    });
});


showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');

        }
    });
}


jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    return false;
}



function sendData() {
    var model = {}
    var SchoolModel = {};
    var SList = [];
    setCities()
    model["Name"] = $("#name").val();
    model["Surname"] = $("#surname").val()
    model["Birthday"] = $("#birthday").val()
    model["Description"] = $("#description").val()
    model["CityId"]= $("cities").val()

    $("[id^=okulDropDownDivId]").each(function (i, e) {

        SchoolModel["SchoolName"] = $(e).find("[id^='graduatedSchoolName']").val();
        SchoolModel["SchoolTypeId"] = $(e).find("[id^='schoolId']").val();

        SList.push(SchoolModel);
        console.log(SList)
        SchoolModel = {};
    })

    model["SList"] = SList;

    $.ajax({
        url: '/Employees/AddOrEdit',
        type: 'POST',
        dataType: "json",
        data: model,
        async:false,
        success: function (data) {
        },
    });
}



let cities = document.getElementById('cities');
let myoption = document.createElement("option");
myoption.text = "Seçiniz";
myoption.setAttribute("disabled", true);
myoption.setAttribute("selected", true);
myoption.setAttribute("hidden", true);
//cities.options.add(myoption);
function setCities() {
    var countries = document.getElementById("countries");
    var selectedCountryId = countries[countries.selectedIndex].value;
    console.log(selectedCountryId);

    var cities = document.getElementById('cities');
    for (let i = 0; i < cities.options.length; i++) {
        let option = cities.options[i];
        if (option.value.split("-")[1] != selectedCountryId) {
            option.style.display = "none";
        } else {
            option.style.display = "inherit";
        }
    }
}

/*setCities();*/


function UploadFile(id) {
    var fdata = new FormData();
    var fileInput = $('#' + id)[0];
    var file = fileInput.files[0];
    fdata.append("file", file);

    $.ajax({
        type: "POST",
        url: "/Employees/WriteFile/",
        data: fdata,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result != null) {

                console.log(result);
                medya = result
                $("#profilepicturefile").attr("src", result.medyaUrl);
                $("#imgA").show();

            }
            else {
                alertim.toast(siteLang.Hata, alertim.types.danger);
            }
        }
    });
}
//function UploadFile(id) {
//    var fdata = new FormData();
//    var fileInput = $('#' + id)[0];
//    var file = fileInput.files[0];
//    fdata.append("file", file);

//    $.ajax({
//        type: "POST",
//        url: "/Employees/WriteFile/",
//        data: fdata,
//        contentType: false,
//        processData: false,
//        success: function (result) {
//            if (result != null) {

//                console.log(result);
//                medya = result
//                $("#profilepicturefile").attr("src", result.medyaUrl);
//                $("#imgA").show();

//            }
//            else {
//                alertim.toast(siteLang.Hata, alertim.types.danger);
//            }
//        }
//    });
//}

//function GetOkulTurleri(id) {
//    $.ajax({
//        url: '/Employees/GetOkulTurleri',
//        async: false,
//        type: 'GET',
//        dataType: "json",
//        success: function (data) {
//            console.log(data);
//            okulTurler = data;
//            var str = ""
//            $.each(data, function (x, y) {//x 0 1 2 y gelen datanın kendisi
//                str = ` <option value="${y.id}">${y.schoolName}</option>`
//                $("#okulTuru"+id).append(str);
//            });
//        },
//    });
//}
function GetOkulTurleri() {
    $.ajax({
        url: '/Employees/GetOkulTurleri',
        async: false,
        type: 'GET',
        dataType: "json",
        success: function (data) {
            okulTurList = data;
            var str = ""
            $.each(data, function (f, y) {//x 0 1 2 y gelen datanın kendisi
                
                str = ` <option value="${y.id}">${y.schoolName}</option>`
                $("#schoolId"+number).append(str);
            });
        },
    });
}

function getOkul() {
    $.ajax({
        url: '/Employees/GetOkulTurleri',
        async: false,
        type: 'GET',
        dataType: "json",
        success: function (data) {
            appendSchoolData(data);
            console.log(data)
        },
    });
}
function appendSchoolData(data) {
    number++;
    console.log(okulTurList)
    var strId = "schoolId" + number;
    let str1 =  `<div class="form-group" id="okulDropDownDivId${number}">
 <input class="input-block-level" type="text" name="newfield" id="graduatedSchoolName${number}" placeholder="New School">
                       <select id="${strId}"> </select>
<input type="button" value="-" id="btnRemoveSchool${number}" onclick="removeSchool()" class="btn btn-group" /> </div>`

    $("#okulTekrar").append(str1);
    $.each(okulTurList, function (f, y) {
        str = ` <option value="${y.id}">${y.schoolName}</option>`
        $("#schoolId"+number).append(str);
    }); 

}

function removeSchool() {
    var parent = document.getElementById("okulTekrar")
    var id = document.getElementById("okulDropDownDivId" + number)
 

        parent.removeChild(id);
       
        number--;
  
}


function SaveSchool() {
    var model = {};
    var okulList= [];

    for (var i = 1; i <= x; i++) {
        okulList["GraduatedSchoolName"] = $("#graduatedSchoolName" + i).val();
        okulList["SchoolId"] = $("#schoolId" + i).val()
    }
    $.ajax({
        type: "POST",
        url: "/Employees/SaveGratuatedSchool/",
        data: model,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result != null) {
                console.log(result);
                
            }
          
        }
    });
}

function sendSchoolData() {
    var model = {}
    var SchoolModel = {};
    var SList = [];

    $("[id^=okulDropDownDivId]").each(function (i, e) {

        SchoolModel["SchoolName"] = $(e).find("[id^='graduatedSchoolName']").val();
        SchoolModel["SchoolTypeId"] = $(e).find("[id^='schoolId']").val();

        SList.push(SchoolModel);
        console.log(SList)
        SchoolModel = {};
    })
    SList.pop();
    model["SList"] = SList;

    $.ajax({
        url: '/Employees/SaveGratuatedSchool',
        type: 'POST',
        dataType: "json",
        data: model,
        success: function (data) {
        },
    });
}

var schoolModel;

function getSavedSchools(id) {
    setTimeout(function () {
        $.ajax({
            url: '/Employees/SavedEmployeeGratuatedSchool/' + id,
            async: false,
            type: 'POST',
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    schoolModel = data;

                    var parent = document.getElementById("okulDropDownDivId0")
                  
                    for (var i = 0; i < data.length; i++) {
                     
                        $("#graduatedSchoolName" + i).val(data[i].graduatedSchoolName);
                        $("#schoolId" + i).val(data[i].schoolId).change();
                        appendSchoolData(data)
                        
                }

                var id = document.getElementById("okulDropDownDivId" + number)


                id.remove();

                number--;


            },
        });
    },500)
}


