import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActitityUpdateComponent } from './actitity-update.component';

describe('ActitityUpdateComponent', () => {
  let component: ActitityUpdateComponent;
  let fixture: ComponentFixture<ActitityUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ActitityUpdateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ActitityUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
