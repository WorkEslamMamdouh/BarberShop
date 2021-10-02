﻿
$(document).ready(() => {
    //debugger;
    Home.InitalizeComponent();
})

namespace Home {


    var TR_Type = "1";
    var Insert_Type;
    var AccountType: Number = 1;
    var MSG_ID: number;
    var Details_Employee: Array<Table_Tim_work> = new Array<Table_Tim_work>();
    //var Details: Array<Reservations> = new Array<Reservations>();
    var Details: Array<Table_Hagz> = new Array<Table_Hagz>();
    var New_Details: Array<Table_Hagz> = new Array<Table_Hagz>();

    var BilldIData: Array<Table_Hagz> = new Array<Table_Hagz>();
    var Branch: Array<G_Branch> = new Array<G_Branch>();     
    var ReportGrid: JsGrid = new JsGrid();
    var Model: Table_Hagz = new Table_Hagz();
    var User: Userclose = new Userclose();
    var User_EMP: updateemploye = new updateemploye();
    var User_Add: UserAdd = new UserAdd();

    var sys: SystemTools = new SystemTools();

    var txt_Cust_Type: HTMLSelectElement;
    var txt_Branch: HTMLSelectElement;


    

    var btnback: HTMLButtonElement;
    var btnDelete: HTMLButtonElement;
    var btnPrint: HTMLButtonElement;
    var btnAdd: HTMLButtonElement;
    var btnEdit: HTMLButtonElement;
    var btnEnter: HTMLButtonElement;
    var btnChange: HTMLButtonElement;
    var btnExit: HTMLButtonElement;
    var ID_Add_Custmor: HTMLButtonElement;
    var ID_Add_E: HTMLButtonElement;
    var Close_Day: HTMLInputElement;
    var myModal: HTMLDivElement;




    var txt_NAME: HTMLInputElement;


    var txt_MOBILE: HTMLInputElement;


    var searchbutmemreport: HTMLInputElement;

    //chkboxes
    var chkActive: HTMLInputElement;

    var compcode: Number;//SharedSession.CurrentEnvironment.CompCode;
    var IsNew;
    var index;
    //Selecteditem
    var Selecteditem: Array<Table_Hagz>;
    var CustomerIdUpdate: number = 0;

    var CustomerId;

    var sum_balance;

    var Debit;
    var Credit;
    var Valid = 0;

    var SearchDetails;
    var NewDetails = new Array();

    var modal;
    var Modal_close;

    var span;
    var flag_corse;
    var Name = "";
    var Phone = "";
    var Type = "";
    var Message = "";
    var close;
    var veid = 0;
    var ID;
    var after_Corse;
    var ID_One_Cust;
    var Div_Num = 0;
    var flag_chack_Enter: boolean = true;
    var BranchCode = 1;
    export function InitalizeComponent() {

        debugger;
        InitalizeControls();
        InitalizeEvents();
        InitializeGrid();
        Get_Branch();
        cheakcloseDay();
        Display();
        setTime();
        Disbly_Emb();

    }
    function InitalizeControls() {
        //debugger;

        btnExit = document.getElementById("btnExit") as HTMLButtonElement;
        btnDelete = document.getElementById("btnDelete") as HTMLButtonElement;
        btnPrint = document.getElementById("btnPrint") as HTMLButtonElement;
        btnAdd = document.getElementById("btnAdd") as HTMLButtonElement;
        btnChange = document.getElementById("btnChange") as HTMLButtonElement;
        ID_Add_Custmor = document.getElementById("ID_Add_Custmor") as HTMLButtonElement;
        ID_Add_E = document.getElementById("ID_Add_E") as HTMLButtonElement;
        Close_Day = document.getElementById("Close_Day") as HTMLInputElement;
        myModal = document.getElementById("myModal") as HTMLDivElement;



        modal = document.getElementById("myModal");
        Modal_close = document.getElementById("Modal_close");

        span = document.getElementsByClassName("close")[0];

        //btnEdit = document.getElementById("btnedite") as HTMLButtonElement;
        btnEnter = document.getElementById("btnEnter") as HTMLButtonElement;
        //btnback = document.getElementById("btnback") as HTMLButtonElement;

        txt_NAME = document.getElementById("txt_NAME") as HTMLInputElement;
        txt_MOBILE = document.getElementById("txt_NAssME") as HTMLInputElement;
        txt_Cust_Type = document.getElementById("txt_Cust_Type") as HTMLSelectElement;
        txt_Branch = document.getElementById("txt_Branch") as HTMLSelectElement;

        
        searchbutmemreport = document.getElementById("searchbutmemreport") as HTMLInputElement;

        ////textBoxes


    }
    function InitalizeEvents() {
        debugger

        btnDelete.onclick = btnDelete_onclick;
        btnPrint.onclick = btnPrint_onclick;
        btnAdd.onclick = btnAdd_onclick;
        btnChange.onclick = btnChange_onclick;
        btnExit.onclick = btnfinish_onclick;
        span.onclick = span_onclick;
        Modal_close.onclick = window_onclick;
        btnEnter.onclick = btnEnter_onClick;
        ID_Add_Custmor.onclick = ID_Add_Custmor_onClick;
        ID_Add_E.onclick = ID_Add_E_onClick;
        Close_Day.onchange = Close_Day_onClick;
        txt_Branch.onchange = txt_Branch_onchange;
        //btnback.onclick = btnback_onclick;
        //btnEdit.onclick = btnEdit_onclick;
        searchbutmemreport.onkeyup = _SearchBox_Change;
        $('.jq-tab-title').on('click', click_Corse);

    }

    //----------------------------------------------Grid-------------------  
    function InitializeGrid() {
        ReportGrid.ElementName = "ReportGrid";
        ReportGrid.PrimaryKey = "ID";
        ReportGrid.Paging = true;
        ReportGrid.PageSize = 50;
        ReportGrid.Sorting = true;
        ReportGrid.Editing = true;
        ReportGrid.Inserting = false;
        ReportGrid.OnRowDoubleClicked = DriverDoubleClick;
        ReportGrid.SelectedIndex = 1;
        ReportGrid.OnItemEditing = () => { };
        ReportGrid.Columns = [
            { title: "ID", name: "ID", type: "text", width: "60px", visible: false },
            { title: "ID", name: "ID", type: "text", width: "60px", visible: false },
            { title: "الرقم", name: "Num", type: "text", width: "30px", textField: "" },
            { title: "الاسم", name: "Name", type: "text", width: "80px" },
            { title: "رقم الجوال", name: "Phone", type: "text", width: "80px" },
            { title: "النوع", name: "NameType", type: "text", width: "60px" },
            { title: "الوقت", name: "RegistredTime", type: "text", width: "60px" },
            { title: "الحاله", name: "Status", type: "text", width: "40px" },
        ];

    }
    function DriverDoubleClick() {

        debugger

        Selecteditem = Details.filter(x => x.ID == Number(ReportGrid.SelectedKey));

        show_div();
        $("#Customer_Detils").removeClass("display_none");
        $("#Add_Customer").addClass("display_none");

        var title = "رقم الزبون ( " + Selecteditem[0].Num + " )";
        var Labol = Selecteditem[0].Name;
        var img = "";
        var id_title = document.getElementById('id_title');
        var id_Labol = document.getElementById('id_Labol');


        id_title.innerHTML = title;
        id_Labol.innerHTML = Labol;

        chack_Enter_Cust();
    }
    function _SearchBox_Change() {
        debugger;

        if (searchbutmemreport.value != "") {



            let search: string = searchbutmemreport.value.toLowerCase();
            SearchDetails = Details.filter(x => x.Num.toString().search(search) >= 0 || x.Name.toLowerCase().search(search) >= 0 /*|| x.PhoneNumber.toLowerCase().search(search) >= 0*/);

            ReportGrid.DataSource = SearchDetails;
            ReportGrid.Bind();
        } else {
            ReportGrid.DataSource = Details;
            ReportGrid.Bind();
        }
    }

    function Get_Branch() {

        Ajax.Callsync({
            type: "Get",
            url: sys.apiUrl("Home", "GetBranch"),   
            success: (d) => {
                let result = d as BaseResponse;
                if (result.IsSuccess) {    
                    Branch = result.Response as Array<G_Branch>;
                    txt_Branch.innerHTML = "";
                    for (var i = 0; i < Branch.length; i++) {    
                        $('#txt_Branch').append('<option  value="' + Branch[i].BranchCode + '">' + Branch[i].NameA + '</option>');    
                    }                                                       


                    BranchCode = Number(txt_Branch.value);
                                         
                }

            }
        });

    }
    function txt_Branch_onchange() {

        BranchCode = Number(txt_Branch.value);

        cheakcloseDay();
        Display();  
        Disbly_Emb();

    }
    //----------------------------------------------------------------------- 

    //----------------------------------------------Display-------------------
    function cheakcloseDay() {

        Ajax.Callsync({
            type: "Get",
            url: sys.apiUrl("Home", "cheakcloseDay"),
            data: { BranchCode: BranchCode },
            success: (d) => {
                let result = d as BaseResponse;
                if (result.IsSuccess) {
                    close = result.Response;

                    if (close == 1) {
                        Close_Day.checked = true;
                        ID_Add_Custmor.disabled = false;
                    }
                    else {
                        Close_Day.checked = false;
                    }


                }

            }
        });

    }
    function setTime() {
        setTimeout(function () {

            Display();
            setTime();
        }, 15000);
    }
    function Display() {
        debugger;
        Close_div();

        Ajax.Callsync({
            type: "Get",
            url: sys.apiUrl("Home", "GetAll"),
            data: { TR_Type: TR_Type, BranchCode: BranchCode },
            success: (d) => {
                debugger;
                let result = d as BaseResponse;
                if (result.IsSuccess) {
                    debugger;
                    Details = result.Response as Array<Table_Hagz>;

                    let id_Corse = 1;
                    Corse_ON_Active();
                    for (var i = 0; i < Details.length; i++) {
                        if (Details[i].cheak == true) {



                            Corse_Is_Active(id_Corse, Details, i);


                            var index = Details.map(function (e) { return e.Num; }).indexOf(Details[i].Num);
                            delete Details[index];

                            id_Corse++;

                            flag_corse = true;
                        }


                    }


                    if (flag_corse == false) {
                        NO_Cust_on_Corse();
                    }


                    reindexArray(Details);



                    for (var i = 0; i < Details.length; i++) {

                        //Details[i].RegistredTime = timeConverter(Details[i].RegistredTime)

                        Details[i].NameType = Details[i].Type == "1" ? 'راجل' : 'طفل';
                        Details[i].Status = Details[i].cheak != true ? 'في الانتظار' : '';

                    }


                    InitializeGrid();
                    ReportGrid.DataSource = Details;
                    ReportGrid.Bind();

                    if (TR_Type == '1') { $('#text_type').html('حجز ( الرجال )'); }
                    else { $('#text_type').html('حجز ( ألاطفال )'); }



                }

            }
        });



        ID_Add_Custmor.disabled = false;
    }
    //----------------------------------------------------------------------- 

    //----------------------------------------------Empl-------------------

    function ID_Add_E_onClick() {
        Div_Num += 1;
        if (Div_Num % 2 == 0) {

            $('#div_Emb').addClass('display_none');

        }
        else {

            $('#div_Emb').removeClass('display_none');

            setTimeout(function () { $('#div_Emb').addClass('display_none'); Div_Num += 1; }, 20000);

        }

    }


    function BuildControls(cnt: number) {
        var html;

        html = '<button id="But_' + cnt + '" class="col-sm-2 col-md-2 col-lg-2 col-xl-2 checkbox_Div">' +
            '<input id="id_' + cnt + '" class=" display_none  " type = "text"  /> ' +
            '<input id="checkbox_' + cnt + '" Data-Num="' + cnt + '" class="col-sm-2 col-md-2 col-lg-2 col-xl-2 checkbox  " type = "checkbox" /> ' +
            '<label id="label_' + cnt + '" class="col-sm-8 col-md-8 col-lg-8 col-xl-8 checkbox_label  " > سامح البحيري  ' +
            '</button > ';


        $("#div_Emb").append(html);

        let checkbox: HTMLInputElement = document.getElementById("checkbox_" + cnt) as HTMLInputElement;

        checkbox.onchange = checkbox_onchange;
    }


    function Disbly_Emb() {

        debugger
        $("#div_Emb").html("");
        Details_Employee = new Array<Table_Tim_work>();
        Ajax.Callsync({
            type: "Get",
            url: sys.apiUrl("Home", "GetAllTable_Tim_work"),
            data: { BranchCode: BranchCode },
            success: (d) => {
                debugger;
                let result = d as BaseResponse;
                Details_Employee = result.Response as Array<Table_Tim_work>;


                for (var i = 0; i < Details_Employee.length; i++) {

                    BuildControls(i);

                    $('#id_' + i).val(Details_Employee[i].ID)
                    $('#label_' + i).html(Details_Employee[i].Name)

                    Details_Employee[i].Cheak == true ? $('#checkbox_' + i).prop("checked", true) : $('#checkbox_' + i).prop("checked", false);




                }

            }
        });
    }


    function checkbox_onchange() {

        let Num = $(this).attr('Data-Num');


        let Id = Number($('#id_' + Num).val());
        let IsAvailable = Number($(this).is(':checked'));

        let Name = $('#label_' + Num).html();

        Ajax.Callsync({
            type: "Get",
            url: sys.apiUrl("Home", "UpdateTable_Tim_work"),
            data: { Cheak: IsAvailable, ID: Id },
            success: (d) => {

                alert('تم تعديل ( ' + Name + ' )');

            }
        });

    }


    //----------------------------------------------------------------------- 

    //----------------------------------------------Change Man or chrde-------------------
    function btnChange_onclick() {
        debugger
        if (TR_Type == "2") {
            TR_Type = "1"; btnChange.innerHTML = '<span class="glyphicon"> <img class="img-fluid" src="/img/testimonial/t1.png" alt="" style="border-radius: 4.25rem;box-shadow: 0 5px 15px rgb(255 165 0 / 75%);" ></span><span > <<< </span>الذهب الي حجز الاطفال';



        }
        else {
            TR_Type = "2"; btnChange.innerHTML = '<span class="glyphicon"> <img class="img-fluid" src="/img/testimonial/t2.png" alt="" style="border-radius: 4.25rem;box-shadow: 0 5px 15px rgb(255 165 0 / 75%);" ></span><span > <<< </span>الذهب الي حجز الرجال';



        }

        Display();
    }
    //-----------------------------------------------------------------------

    //----------------------------------------------Add_Cust------------------- 
    function ID_Add_Custmor_onClick() {

        if (close == 0) {
            alert('لا يمكنك اضافة زبون لانه المحل مغلق يجب فتح المحل اولا')
        }
        else {
            ID_Add_Custmor.disabled = true;
            insert_Table();
            //show_div();
            //$("#Add_Customer").removeClass("display_none");
            //$("#Customer_Detils").addClass("display_none");
        }

    }
    function btnAdd_onclick() {
        if (close == 0) {
            alert('لا يمكنك اضافة زبون لانه المحل مغلق يجب فتح المحل اولا')
        }
        else {

            insert_Table();
        }
    }
    function Assign_Insert() {

        let ph = "010" + (Math.floor(Math.random() * 10000) + 1) + (Math.floor(Math.random() * 10000) + 1)
        if (txt_NAME.value == "") { Name = "اسمه أيه..." } else { Name = txt_NAME.value; }
        if (txt_MOBILE.value == "") { Phone = ph } else { Phone = txt_MOBILE.value; }
        TR_Type == '1' ? (Type = "1", Insert_Type = 1) : (Type = "2", Insert_Type = 2)

        Message = "Eslam";


        //if (txt_Cust_Type.value == "Null") {
        //    Errorinput(txt_Cust_Type);
        //    alert('يجب اختيار النوع');

        //    return veid = 1;
        //}
        //else {

        //    let ph = "010" + (Math.floor(Math.random() * 10000) + 1) + (Math.floor(Math.random() * 10000) + 1)
        //    if (txt_NAME.value == "") { Name = "اسمه اية..." } else { Name = txt_NAME.value; }
        //    if (txt_MOBILE.value == "") { Phone = ph } else { Phone = txt_MOBILE.value; }
        //    if (txt_Cust_Type.value == "0") { Type = "1"; Insert_Type = 1; }
        //    if (txt_Cust_Type.value == "1") { Type = "2"; Insert_Type = 2; }

        //    Message = "Eslam";
        //}

        return veid = 0;

    }
    function insert_Table() {
        debugger
        Assign_Insert();
        if (veid != 1) {

            Ajax.CallAsync({
                type: "Get",
                url: sys.apiUrl("Home", "PROC_insert_Table"),
                data: { Name: Name, Phone: Phone, Type: Type, Message: Message, TR_Type: Insert_Type, BranchCode: BranchCode },
                success: (d) => {

                    $('#ID_Add_Custmor').removeClass('display_none');

                    Display();

                    let Index = Details.length - 1;

                    Print(Details[Index].RegistredTime, Details[Index].Num, Details[Index].Name, Details[Index].Phone);


                }
            });

        }
    }
    //-----------------------------------------------------------------------

    //----------------------------------------------Corse------------------- 
    function NO_Cust_on_Corse() {
        $('#text_Corse').html('<br />لا يوجد اي زبون قاعد علي الكورسي');
        $('#disc_Corse').attr('style', 'margin-top: 38px;text-align: center;height: 135px;box-shadow: 0 5px 15px rgb(255 255 255);font-size: x-large;');
        $('#btnExit').attr('style', 'display: none;');
    }
    function Corse_Is_Active(id_Corse: number, Details: Array<Table_Hagz>, i: number) {
        $('#Corse_' + id_Corse + '').attr('class', ' col-sm-3 col-md-3 col-lg-3 col-xl-3  jq-tab-title Corse_Is_Active');
        $('#Corse_' + id_Corse + '').attr('Data_ID', Details[i].ID);
        $('#Corse_' + id_Corse + '').attr('StatusId', '' + Details[i].cheak + '');
        $('#Corse_' + id_Corse + '').attr('Num', Details[i].Num);
        $('#Corse_' + id_Corse + '').attr('Phone', Details[i].Phone);
        $('#Corse_' + id_Corse + '').attr('DesName', Details[i].Name);
        //let timer = timeConverter(Details[i].RegistredTime)
        $('#Corse_' + id_Corse + '').attr('Time', Details[i].RegistredTime);
        $('#Corse_' + id_Corse + '').attr('Message', '');
        $('#Corse_' + id_Corse + '').attr('cheak', '' + Details[i].cheak + '');

        $('#text_Num_' + id_Corse + '').html('' + Details[i].Num + '');


        $('#disc_Corse').attr('style', '');
        $('#btnExit').attr('style', 'background-color: #b72020;width: 261px;font-size: 31px;');


        after_Corse = $('#Corse_1');

        //after_Corse = $(this); 
        //alert($(this).attr('Time'));

        after_Corse.attr('style', 'background: #00ffa1cc !important;border-radius: 46px 50px 70px 68px; box-shadow: 0 5px 15px rgb(0 255 55);height: 108px;');

        $('#text_Corse').html('( ' + after_Corse.attr('Time') + ' ) الرقم ( ' + after_Corse.attr('num') + ' )  <br />  ( ' + after_Corse.attr('desname') + ' ) الاسم <br />  ( ' + after_Corse.attr('phone') + ' ) التليفون <br /> ');

        $('#btnExit').attr('Data_ID', after_Corse.attr('Data_ID'));
        $('#btnExit').attr('StatusId', after_Corse.attr('StatusId'));

    }
    function Corse_ON_Active() {
        for (var i = 1; i < 5; i++) {
            $('#Corse_' + i + '').attr('class', 'jq-tab-title Corse_ON_Active');
            $('#Corse_' + i + '').attr('Num', '');
            $('#Corse_' + i + '').attr('Phone', '');
            $('#Corse_' + i + '').attr('DesName', '');
            $('#Corse_' + i + '').attr('Message', '');
            $('#Corse_' + i + '').attr('cheak', 'false');
            $('#Corse_' + i + '').attr('style', '');
            $('#text_Num_' + i + '').html('');
        }
        flag_corse = false;


        if (TR_Type == "1") {

            for (var i = 1; i < 5; i++) {
                $('#Corse_' + i + '').removeClass('display_none');
            }

        }
        else {
            for (var i = 2; i < 5; i++) {
                $('#Corse_' + i + '').addClass('display_none');
            }
        }

    }
    function click_Corse() {
        if ($(this).attr('num') != '') {

            after_Corse.attr('style', '');
            after_Corse = $(this);

            $(this).attr('style', 'background: #00ffa1cc !important;border-radius: 46px 50px 70px 68px; box-shadow: 0 5px 15px rgb(0 255 55);height: 108px;');

            $('#text_Corse').html('( ' + after_Corse.attr('Time') + ' ) الرقم ( ' + $(this).attr('num') + ' ) <br />  ( ' + $(this).attr('desname') + ' ) الاسم <br />  ( ' + $(this).attr('phone') + ' ) التليفون <br /> ');

            $('#btnExit').attr('Data_ID', $(this).attr('Data_ID'));

        }
    }
    //----------------------------------------------------------------------- 

    //----------------------------------------------finish------------------- 
    function btnfinish_onclick() {
        debugger

        var r = confirm('هل انتها من الحلاقه');
        if (r == true) {

            let id = Number($(this).attr('Data_ID'));
            Ajax.Callsync({
                type: "Get",
                url: sys.apiUrl("Home", "PROC_Delete_Rows"),
                data: { ID: id, BranchCode: BranchCode },
                success: (d) => {
                    Display();


                }
            });
        }
        else {

        }


    }
    //----------------------------------------------------------------------- 


    //----------------------------------------------------Poup--------------------------
    function btnPrint_onclick() {

        Print(Selecteditem[0].RegistredTime, Selecteditem[0].Num, Selecteditem[0].Name, Selecteditem[0].Phone);
    }
    function btnDelete_onclick() {

        var r = confirm('هل تريد المسح');
        if (r == true) {

            let Id = Selecteditem[0].ID;

            Ajax.Callsync({
                type: "Get",
                url: sys.apiUrl("Home", "PROC_Delete_Rows"),
                data: { ID: Id, BranchCode: BranchCode },
                success: (d) => {

                    Display();

                }
            });


        }
        else {

        }
    }
    function btnEnter_onClick() {
        debugger
        //var validation_cheak = Details.filter(x => x.cheak == true);
        //if (validation_cheak.length < 4) {
        //    debugger
        //    Enter_Customer(Selecteditem[0].ID);
        //    //Enter_Rows(Selecteditem[0].Num);
        //}
        //else {
        //    alert("العدد مكتمل يجب اخراج احد");
        //}

        //flag_chack_Enter = true;


        //if (!chack_Enter_Cust()) {

        //    Assign_Insert();
        //    if (veid != 1) {

        //        User_Add = new UserAdd();

        //        User_Add.FullName = Name;
        //        User_Add.PhoneNumber = Phone;
        //        User_Add.ServiceId = Number(Type);

        //        Ajax.Callsync({
        //            type: "POST",
        //            url: (url + '/api/BarberReservation/NewClientWithConfirmation'),
        //            data: JSON.stringify(User_Add),
        //            success: (d) => {

        //                flag_chack_Enter = false;


        //            }
        //        });


        //    }

        //}


        var validation_cheak;
        var flag_cheak = 0;
        for (var i = 1; i < 5; i++) {
            validation_cheak = $('#Corse_' + i + '').attr('cheak');
            if (validation_cheak == "true") {
                flag_cheak += 1;
            }

        }


        if (TR_Type == "1") {

            if (flag_cheak < 4) {
                debugger
                Enter_Customer(Selecteditem[0].ID);
                Close_div();
            }
            else {
                alert(" العدد مكتمل في حجز الرجال يجب اخراج احد من علي الكرسي");
            }

        }
        else {

            if (flag_cheak < 1) {
                debugger
                Enter_Customer(Selecteditem[0].ID);
                Close_div();
            }
            else {
                alert("العدد مكتمل في حجز الاطفال يجب اخراج احد من علي الكرسي");
            }

        }





    }
    function Enter_Customer(New_ID: number) {

        Ajax.Callsync({
            type: "Get",
            url: sys.apiUrl("Home", "PROC_Enter_Customer"),
            data: { ID: New_ID, TR_Type: TR_Type, BranchCode: BranchCode},
            success: (d) => {

                Display();

            }
        });

    }
    function chack_Enter_Cust() {

        if (Details.length <= 2) {

            //alert('اصغر' + Details.length)
            return false;
        }
        else {
            //alert('اكبر تمام' + Details.length)

            return true
        }

    }

    function span_onclick() {
        Close_div();
    }
    function window_onclick() {
        debugger
        Close_div();
    }
    function show_div() {
        modal.style.display = "block";
        Modal_close.style.display = "block";
    }
    function Close_div() {
        modal.style.display = "none";
        Modal_close.style.display = "none";
    }
    //------------------------------------------------------------------------------------

    //----------------------------------------------------Close_Day-------------------------- 
    function Close_Day_onClick() {

        var r;
        let ok;
        if (Close_Day.checked == false) {
            r = confirm('هل تريد اغلاق المحل');
            ok = 0;
        }
        else {
            r = confirm('هل تريد فتح المحل');
            ok = 1;
        }

        if (r == true) {

            Ajax.Callsync({
                type: "Get",
                url: sys.apiUrl("Home", "closeDay"),
                data: { BranchCode: BranchCode },
                success: (d) => {

                    if (ok == 0) {
                        close = 0

                    }
                    else {
                        close = 1;
                    }
                    Display();

                    setTime();

                }
            });

        } else {

            if (Close_Day.checked == false) {
                Close_Day.checked = true;
            }
            else {
                Close_Day.checked = false;
            }


        }
    }
    //------------------------------------------------------------------------------------

    function Print(date, number, name, phone) {

        debugger
        var divToPrint = document.getElementById('printer');
        document.getElementById('date').innerHTML = date;
        document.getElementById('number').innerHTML = number;
        document.getElementById('name').innerHTML = name;
        document.getElementById('phone').innerHTML = phone;

        var newWin = window.open('', 'Print-Window', 'height=600,width=800');

        //newWin.document.open();
        this.prints = true;

        newWin.document.write('<body onload="window.print()" style="width:220px;height:10px!important;" >');
        newWin.document.write(divToPrint.innerHTML);
        newWin.document.write('</body></html>');

        newWin.focus();
        newWin.print();

        setTimeout(function () { newWin.close(); }, 10);




    }




}






