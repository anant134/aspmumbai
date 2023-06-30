import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DownloadVacanciesComponent } from './download-vacancies.component';

describe('DownloadVacanciesComponent', () => {
  let component: DownloadVacanciesComponent;
  let fixture: ComponentFixture<DownloadVacanciesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DownloadVacanciesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DownloadVacanciesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
