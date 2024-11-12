import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingAddComponent } from './booking-add.component';

describe('BookingAddComponent', () => {
  let component: BookingAddComponent;
  let fixture: ComponentFixture<BookingAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookingAddComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookingAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
