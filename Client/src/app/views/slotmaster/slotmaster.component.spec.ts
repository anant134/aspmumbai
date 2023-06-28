import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SlotmasterComponent } from './slotmaster.component';

describe('SlotmasterComponent', () => {
  let component: SlotmasterComponent;
  let fixture: ComponentFixture<SlotmasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SlotmasterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SlotmasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
