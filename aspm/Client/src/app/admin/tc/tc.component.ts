import { Component, OnInit, TemplateRef } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { MessageService } from "primeng/api";
import { ITC } from "../../models/tc.models";
import { commonService } from "../../service/commonService-service";

@Component({
  selector: "app-tc",
  templateUrl: "./tc.component.html",
  styleUrls: ["./tc.component.scss"],
  providers: [MessageService],
})
export class TCComponent implements OnInit {
  displayConfirmation: any = false;
  modalRef: BsModalRef;
  modelpopuptitle: any;
  tcdata: any;
  deleteinfo: any;
  // tcdata: ITC;
  ClsClass: any[];
  ClsList: any[] = [
    { name: "1", code: 1 },
    { name: "2", code: 2 },
    { name: "3", code: 3 },
    { name: "4", code: 4 },
  ];
  SessionYearList: any[] = [{ name: "2000-2001", code: "2000-2001" }];
  formtc = new FormGroup({
    admno: new FormControl("", [Validators.required]),
    name: new FormControl("", [Validators.required]),
    fathername: new FormControl("", [Validators.required]),
    selectedClass: new FormControl(""),
    tcno: new FormControl("", [Validators.required]),
    id: new FormControl(""),
    selectedlist: new FormControl(""),
    selectedSessionYearList: new FormControl(""),
  });
  constructor(
    private modalService: BsModalService,
    private commonService: commonService,
    private messageService: MessageService
  ) {}
  get f() {
    return this.formtc.controls;
  }
  ngAfterViewInit(): void {
    //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
    //Add 'implements AfterViewInit' to the class.
  }
  ngOnInit(): void {
    this.ClsClass = [
      { name: "I", code: 1 },
      { name: "II", code: 2 },
      { name: "III", code: 3 },
      { name: "Iv", code: 4 },
      { name: "V", code: 5 },
      { name: "VI", code: 6 },
      { name: "VII", code: 7 },
      { name: "VIII", code: 8 },
      { name: "IX", code: 9 },
      { name: "X", code: 10 },
      { name: "XI", code: 11 },
      { name: "XII", code: 12 },
      { name: "XIII", code: 13 },
      { name: "XIV", code: 14 },
      { name: "XV", code: 15 },
      { name: "XVI", code: 16 },
      { name: "XVII", code: 17 },
      { name: "XVIII", code: 18 },
      { name: "XIX", code: 19 },
      { name: "XX", code: 20 },
    ];
    this.getAllTC();
    this.SessionYearList = [];
    for (let index = 2000; index < 2035; index++) {
      this.SessionYearList.push({
        name: index + "-" + (index + 1),
        code: index,
      });
    }
    this.setDefaultValue();
  }
  setDefaultValue() {
    this.formtc.controls.id.setValue(0);
    this.formtc.controls.selectedSessionYearList.setValue(
      this.SessionYearList[0].code
    );
    this.formtc.controls.selectedClass.setValue(this.ClsClass[0].code);
    this.formtc.controls.selectedlist.setValue(this.ClsList[0].code);
  }
  getAllTC() {
    this.commonService.get({}, "/TC/GetAllTcs").subscribe((res: any) => {
      if (res.result == 1) {
        debugger;
        this.tcdata = res.resutlData;
      }
    });
  }
  openModal(template: TemplateRef<any>, itemdata: any) {
    this.formtc.reset();
    this.setDefaultValue();
    this.modelpopuptitle = "Add TC";
    if (itemdata.id != undefined) {
      this.modelpopuptitle = "Edit TC";
      this.updateFormData(itemdata);
    }
    this.modalRef = this.modalService.show(template, {
      class: "modal-xl",
      animated: true,
      backdrop: "static",
    });
  }
  updateFormData(data) {
    // admno: new FormControl("", [Validators.required]),
    // name: new FormControl("", [Validators.required]),
    // fathername: new FormControl("", [Validators.required]),
    // selectedClass: new FormControl(""),
    // tcno: new FormControl("", [Validators.required]),
    // id: new FormControl(""),
    // selectedlist: new FormControl(""),
    // selectedSessionYearList: new FormControl(""),
    this.formtc.controls.id.setValue(data.id);
    this.formtc.controls.admno.setValue(data.admNo);
    this.formtc.controls.fathername.setValue(data.fatherName);
    this.formtc.controls.fathername.setValue(data.fatherName);
    this.formtc.controls.name.setValue(data.name);
    this.formtc.controls.tcno.setValue(data.tcNo);
    this.formtc.controls.selectedlist.setValue(data.admNo);
    var ss = this.SessionYearList.filter((x) => x.name == data.sessionYear);
    this.formtc.controls.selectedSessionYearList.setValue(ss[0].code);
  }
  onChange(event) {
    debugger;
    console.log("event :" + event);
    console.log(event.value);
  }
  onSubmit() {
    if (this.formtc.invalid) {
      return;
    } else {
      var ss = this.SessionYearList.filter(
        (x) => x.code == this.formtc.controls.selectedSessionYearList.value
      );

      var sendata = {
        id:
          this.formtc.controls.id.value == ""
            ? 0
            : parseInt(this.formtc.controls.id.value),
        listId:
          this.formtc.controls.selectedlist.value == ""
            ? 0
            : parseInt(this.formtc.controls.selectedlist.value),
        admNo: parseInt(this.formtc.controls.admno.value),
        name: this.formtc.controls.name.value,
        class:
          this.formtc.controls.selectedClass.value == ""
            ? 0
            : this.formtc.controls.selectedClass.value.toString(),
        fatherName: this.formtc.controls.fathername.value,
        tcNo:
          this.formtc.controls.tcno.value == ""
            ? 0
            : parseInt(this.formtc.controls.tcno.value),
        sessionYear: ss[0].name,

        createdOn: "2022-08-22T16:04:28.683Z",
        createdBy: 0,
        modifiedBy: 0,
        modifiedOn: "2022-08-22T16:04:28.683Z",
        isActive: true,
      };
      console.log(sendata);
      this.commonService.post(sendata, "/Tc/SaveTC").subscribe(
        (res) => {
          if (res.result == 1) {
            debugger;
            var ids =
              this.formtc.controls.id.value == ""
                ? 0
                : parseInt(this.formtc.controls.id.value);
            var message = "Record added successfully";
            if (ids > 0) {
              message = "Record updated successfully";
            }
            this.showMessage("success", "Success", message);
          } else {
            this.showMessage("error", "Error", res.message);
          }
          this.modalService.hide();
          this.getAllTC();
        },
        (error) => {
          this.showMessage("error", "Error", error.error.message);
        },
        () => {
          // No errors, route to new page
        }
      );
    }
  }
  showConfrimationDialog(data) {
    this.deleteinfo = data;
    this.displayConfirmation = true;
  }
  deleteTc() {
    this.commonService
      .post({ id: this.deleteinfo.id }, "/TC/deleteTC")
      .subscribe((res: any) => {
        if (res.result == 1) {
          debugger;
          this.displayConfirmation = false;
          this.getAllTC();
          this.showMessage("success", "Success", "Record Deleted Successfully");
        }
      });
  }
  showMessage(severity, summary, message) {
    this.messageService.add({
      severity: severity,
      summary: summary,
      detail: message,
    });
  }
}
