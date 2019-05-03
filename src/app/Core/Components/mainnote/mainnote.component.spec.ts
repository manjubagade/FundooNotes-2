import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MainnoteComponent } from './mainnote.component';

describe('MainnoteComponent', () => {
  let component: MainnoteComponent;
  let fixture: ComponentFixture<MainnoteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MainnoteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MainnoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
