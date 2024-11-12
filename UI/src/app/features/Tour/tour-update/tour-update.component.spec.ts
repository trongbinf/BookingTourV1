import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TourUpdateComponent } from './tour-update.component';

describe('TourUpdateComponent', () => {
  let component: TourUpdateComponent;
  let fixture: ComponentFixture<TourUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TourUpdateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TourUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
