import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { AdminRoutingModule } from "./admin-routing.module";
import { HomeBannerComponent } from "./home-banner/home-banner.component";
import { NewsEventsComponent } from "./news-events/news-events.component";
import { NoticeBoardComponent } from "./notice-board/notice-board.component";
import { DownloadVacanciesComponent } from "./download-vacancies/download-vacancies.component";
import { StaffComponent } from "./staff/staff.component";

import { TableModule } from "primeng/table";
import { FileUploadModule } from "primeng/fileupload";
import { DialogModule } from "primeng/dialog";
import { ToastModule } from "primeng/toast";
import { TCComponent } from "./tc/tc.component";
import { MessagesModule } from "primeng/messages";
import { MessageModule } from "primeng/message";
import { DropdownModule } from "primeng/dropdown";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CalendarModule } from "primeng/calendar";
import { InputNumberModule } from "primeng/inputnumber";
@NgModule({
  declarations: [
    HomeBannerComponent,
    NewsEventsComponent,
    NoticeBoardComponent,
    DownloadVacanciesComponent,
    StaffComponent,
    TCComponent,
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    AdminRoutingModule,
    TableModule,
    FileUploadModule,
    DialogModule,
    ToastModule,
    MessageModule,
    MessagesModule,
    DropdownModule,
    CalendarModule,
    InputNumberModule,
  ],
})
export class AdminModule {}
