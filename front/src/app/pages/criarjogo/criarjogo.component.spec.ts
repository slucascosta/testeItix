import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CriarjogoComponent } from './criarjogo.component';

describe('CriarjogoComponent', () => {
  let component: CriarjogoComponent;
  let fixture: ComponentFixture<CriarjogoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CriarjogoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CriarjogoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
