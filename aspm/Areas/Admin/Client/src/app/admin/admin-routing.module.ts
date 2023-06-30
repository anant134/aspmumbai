import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { DownloadVacanciesComponent } from "./download-vacancies/download-vacancies.component";
import { HomeBannerComponent } from "./home-banner/home-banner.component";
import { NewsEventsComponent } from "./news-events/news-events.component";
import { NoticeBoardComponent } from "./notice-board/notice-board.component";
import { StaffComponent } from "./staff/staff.component";
import { TCComponent } from "./tc/tc.component";

const routes: Routes = [
  {
    path: "",
    data: {
      title: "Admin",
    },
    children: [
      {
        path: "",
        redirectTo: "homebanner",
      },
      {
        path: "homebanner",
        component: HomeBannerComponent,
        data: {
          title: "Home Banner",
        },
      },
      {
        path: "newsevents",
        component: NewsEventsComponent,
        data: {
          title: "News",
        },
      },
      {
        path: "noticeboard",
        component: NoticeBoardComponent,
        data: {
          title: "Notice board",
        },
      },
      {
        path: "downloadvacancies",
        component: DownloadVacanciesComponent,
        data: {
          title: "Download & Vacancies",
        },
      },
      {
        path: "staff",
        component: StaffComponent,
        data: {
          title: "Our staff",
        },
      },
      {
        path: "tc",
        component: TCComponent,
        data: {
          title: "TC",
        },
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
