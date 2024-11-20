import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListTourSearchComponent } from './list-tour-search.component';

describe('ListTourSearchComponent', () => {
  let component: ListTourSearchComponent;
  let fixture: ComponentFixture<ListTourSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListTourSearchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListTourSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
