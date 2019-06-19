import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayLabelsComponent } from './display-labels.component';

describe('DisplayLabelsComponent', () => {
  let component: DisplayLabelsComponent;
  let fixture: ComponentFixture<DisplayLabelsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DisplayLabelsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DisplayLabelsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
