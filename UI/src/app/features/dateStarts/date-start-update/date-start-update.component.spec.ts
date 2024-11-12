import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateStartUpdateComponent } from './date-start-update.component';

describe('DateStartUpdateComponent', () => {
  let component: DateStartUpdateComponent;
  let fixture: ComponentFixture<DateStartUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DateStartUpdateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DateStartUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
