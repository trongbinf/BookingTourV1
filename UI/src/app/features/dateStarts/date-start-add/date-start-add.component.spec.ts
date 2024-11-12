import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateStartAddComponent } from './date-start-add.component';

describe('DateStartAddComponent', () => {
  let component: DateStartAddComponent;
  let fixture: ComponentFixture<DateStartAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DateStartAddComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DateStartAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
