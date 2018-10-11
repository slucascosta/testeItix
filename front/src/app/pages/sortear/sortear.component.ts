import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { interval } from 'rxjs';

@Component({
  selector: 'app-sortear',
  templateUrl: './sortear.component.html',
  styleUrls: ['./sortear.component.css']
})
export class SortearComponent implements OnInit {

  @ViewChild('name') btnSortear: ElementRef;

  private sorteado: Array<string> = new Array(6).fill('00');
  private listMega: Array<object> = [];
  private listQuina: Array<object> = [];
  private listQuadra: Array<object> = [];

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    
  }

  sortearNumero (el) {
    this.limpar();

    this.apiService.sortear().subscribe(
      res => {
        this.exibirGanhadore(res);
        this.btnSortear.nativeElement.disabled = true;
      }
    );
  }

  exibirGanhadore (res) {
    let jogo = res.Numero.split(', ');

    this.exibirNumeroMega(jogo, 0,
      () => {
        this.listMega = [ res ];
        this.carregarQuina(res.Id);
        this.carregarQuadra(res.Id);
      });
  }

  exibirNumeroMega (jogo, i, callback) {
    if (i == jogo.length) {
      return;
    }

    this.sorteado[i] = jogo[i].padStart(2, "0");

    if (i == jogo.length - 1) {
      this.btnSortear.nativeElement.disabled = false;
      callback();
      return;      
    }

    setTimeout(() => {
      this.exibirNumeroMega(jogo, ++i, callback);
    }, 300);
  }

  carregarQuina (id) {
    this.apiService.obterGanhadoresQuina(id).subscribe(
      (res: Array<object>) => this.listQuina = res
    );
  }

  carregarQuadra (id) {
    this.apiService.obterGanhadoresQuadra(id).subscribe(
      (res: Array<object>) => this.listQuadra = res
    );
  }

  limpar () {
    this.sorteado.fill("00");
    this.listMega = [];
    this.listQuina = [];
    this.listQuadra = [];
  }
}
