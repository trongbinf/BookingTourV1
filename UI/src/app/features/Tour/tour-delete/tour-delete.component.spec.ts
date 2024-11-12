import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TourDeleteComponent } from './tour-delete.component';

describe('TourDeleteComponent', () => {
  let component: TourDeleteComponent;
  let fixture: ComponentFixture<TourDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TourDeleteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TourDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
