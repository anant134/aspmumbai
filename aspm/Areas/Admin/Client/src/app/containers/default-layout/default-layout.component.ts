import { Component } from "@angular/core";
import { navItems } from "../../_nav";
import { ActivatedRoute, Router } from "@angular/router";
import { Location } from "@angular/common";

import { AuthGuradServiceService } from "./../../auth-gurad-service.service";

declare let $: any;
@Component({
  selector: "app-dashboard",
  templateUrl: "./default-layout.component.html",
})
export class DefaultLayoutComponent {
  public sidebarMinimized = false;
  public navItems = navItems;
  public userSessionInfo: any;
  constructor(
    private router: Router,
    private location: Location,
    private Authguardservice: AuthGuradServiceService
  ) {}
  ngAfterViewInit() {
    alert("1");
    if ($("body").hasClass("header-fixed")) {
      $("body").removeClass("header-fixed");
    }
  }
  ngOnInit() {
    // debugger;
    this.navItems = [
      {
        name: "Home-Banner",
        url: "/admin/homebanner",
        icon: "icon-calculator",
      },
      {
        name: "News Events",
        url: "/admin/newsevents",
        icon: "icon-calculator",
      },
      {
        name: "TC",
        url: "/admin/tc",
        icon: "icon-calculator",
      },
    ];
    // if (!this.Authguardservice.gettoken()) {
    //   this.router.navigateByUrl("/login");
    // } else {
    //   this.userSessionInfo =
    //     sessionStorage.length > 0
    //       ? JSON.parse(sessionStorage.getItem("userdata"))
    //       : [];

    //   //if admin show all

    //   // this.navItems.push(
    //   //   {
    //   //     name: "TC",
    //   //     url: "/tc",
    //   //     icon: "icon-calculator",
    //   //   }
    //   //   // {
    //   //   //   name: "Country Master",
    //   //   //   url: "/countrymaster",
    //   //   //   icon: "icon-calculator",
    //   //   // },
    //   //   // {
    //   //   //   name: "Municiplality Master",
    //   //   //   url: "municiplalitymaster",
    //   //   //   icon: "icon-calculator",
    //   //   // },
    //   //   // {
    //   //   //   name: "Barangay Master",
    //   //   //   url: "barangaymaster",
    //   //   //   icon: "icon-calculator",
    //   //   // },
    //   //   // {
    //   //   //   name: "Province Master",
    //   //   //   url: "/provincemaster",
    //   //   //   icon: "icon-calculator",
    //   //   // },
    //   //   // {
    //   //   //   name: "Holiday Master",
    //   //   //   url: "/holidaymaster",
    //   //   //   icon: "icon-calculator",
    //   //   // },
    //   //   // {
    //   //   //   name: "Device Master",
    //   //   //   url: "/devicemaster",
    //   //   //   icon: "icon-calculator",
    //   //   // },
    //   //   // {
    //   //   //   name: "Report",
    //   //   //   url: "",
    //   //   //   icon: "",
    //   //   // }
    //   // );
    // }
  }
  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }
  LogoutUser() {
    //debugger;
    sessionStorage.removeItem("userdata");
    this.location.replaceState("/"); // clears browser history so they can't navigate with back button
    // this.router.navigate(["login"]);
  }
}
