import { Component, OnInit, TemplateRef } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { MessageService } from "primeng/api";
import { FileUploadModule } from "primeng/fileupload";
import { commonService } from "../../service/commonService-service";
@Component({
  selector: "app-home-banner",
  templateUrl: "./home-banner.component.html",
  styleUrls: ["./home-banner.component.scss"],
  providers: [MessageService],
})
export class HomeBannerComponent implements OnInit {
  modalRef: BsModalRef;
  modelpopuptitle: any;
  uploadedFiles: any[] = [];
  banners: any = [];
  displayConfirmation: any = false;
  deletebannerinfo: any;
  constructor(
    private modalService: BsModalService,
    private commonService: commonService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.getBanners();
  }
  showConfrimationDialog(data) {
    this.deletebannerinfo = data;
    this.displayConfirmation = true;
  }
  deleteBanner() {
    this.commonService
      .post({ id: this.deletebannerinfo.id }, "/homebanner/deleteBanner")
      .subscribe((res: any) => {
        if (res.Result == 1) {
          debugger;
          this.displayConfirmation = false;
          this.getBanners();
        }
      });
  }

  openModal(template: TemplateRef<any>, itemdata: any) {
    this.modelpopuptitle = "Add Banner";
    if (itemdata.id != undefined) {
      this.modelpopuptitle = "Edit Banner";
    }
    this.modalRef = this.modalService.show(template, {
      class: "modal-xl",
      animated: true,
      backdrop: "static",
    });
  }
  onUpload(event) {
    // for (let file of event.files) {
    //   this.uploadedFiles.push(file);
    // }

    this.messageService.add({
      severity: "info",
      summary: "File Uploaded",
      detail: "",
    });
    this.getBanners();
  }
  getBanners() {
    this.commonService
      .get({}, "/homebanner/GetAllBanners")
      .subscribe((res: any) => {
        if (res.Result == 1) {
          debugger;
          this.banners = res.ResutlData;
        }
      });
  }
  showSuccess() {
    this.messageService.add({
      severity: "success",
      summary: "Success",
      detail: "Message Content",
    });
  }
}
