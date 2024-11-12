import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateStartDeleteComponent } from './date-start-delete.component';

describe('DateStartDeleteComponent', () => {
  let component: DateStartDeleteComponent;
  let fixture: ComponentFixture<DateStartDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DateStartDeleteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DateStartDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
