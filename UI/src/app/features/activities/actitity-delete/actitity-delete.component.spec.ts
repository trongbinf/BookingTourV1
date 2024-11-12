import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActitityDeleteComponent } from './actitity-delete.component';

describe('ActitityDeleteComponent', () => {
  let component: ActitityDeleteComponent;
  let fixture: ComponentFixture<ActitityDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ActitityDeleteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ActitityDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
