import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActitityAddComponent } from './actitity-add.component';

describe('ActitityAddComponent', () => {
  let component: ActitityAddComponent;
  let fixture: ComponentFixture<ActitityAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ActitityAddComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ActitityAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
