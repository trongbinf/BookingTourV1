import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TourAddComponent } from './tour-add.component';

describe('TourAddComponent', () => {
  let component: TourAddComponent;
  let fixture: ComponentFixture<TourAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TourAddComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TourAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
