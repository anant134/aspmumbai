import { Component } from "@angular/core";
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";

import { commonService } from "../../service/commonService-service";
import { MessageService } from "primeng/api";

@Component({
  selector: "app-dashboard",
  templateUrl: "login.component.html",
  providers: [MessageService],
})
export class LoginComponent {
  province: any[] = [];
  municiplality: any[] = [];
  barangay: any[] = [];
  form: FormGroup;
  submitted = false;

  selectedProvince: any = null;
  selectedMuniciplality: any = null;
  selectedBarangay: any = null;

  constructor(
    private route: ActivatedRoute,
    private commonService: commonService,
    private formBuilder: FormBuilder,
    private router: Router,
    private messageService: MessageService
  ) {}
  ngOnInit(): void {
    debugger;
    sessionStorage.clear();

    //this.getProvinceAndMuniciplality();
    this.form = this.formBuilder.group({
      username: ["", [Validators.required]],
      password: ["", [Validators.required]],
    });
  }
  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onForgetPassword() {
    this.router.navigate(["forgetpassword"], {});
  }
  onSubmit(): void {
    debugger;
    this.submitted = true;
    if (this.form.invalid) {
      return;
    } else {
      this.commonService
        .post(
          {
            username: this.form.controls.username.value,
            password: this.form.controls.password.value,
          },
          "/User/GetAuth"
        )
        .subscribe(
          (res) => {
            if (res.Result == 1) {
              sessionStorage.setItem(
                "userdata",
                JSON.stringify({
                  isSuperAdmin: "1",
                  name: "anant",
                  emailid: res.ResutlData.Username,
                  roleid: 1,
                })
              );
              this.router.navigate(["admin"], {});
            } else {
              this.messageService.add({
                severity: "error",
                summary: "Error",
                detail: res.message,
              });
            }
          },
          (error) => {
            this.messageService.add({
              severity: "error",
              summary: "Error",
              detail: error.error.message,
            });
          },
          () => {
            // No errors, route to new page
          }
        );

      // sessionStorage.setItem(
      //   "userdata",
      //   JSON.stringify({
      //     isSuperAdmin: "1",
      //     name: "anant",
      //     emailid: "anant@gmail.com",
      //     roleid: 1,
      //   })
      // );
      // this.router.navigate(["dashboard"], {});
    }
  }
}
