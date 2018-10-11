import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-criarjogo',
  templateUrl: './criarjogo.component.html',
  styleUrls: ['./criarjogo.component.css']
})
export class CriarjogoComponent implements OnInit {

  @ViewChild('primDezena') primDezena: ElementRef;
  @ViewChild('btnCancelSurpresinha') btnCancelSurpresinha: ElementRef;
  @ViewChild('pJogoRealizado') pJogoRealizado: ElementRef;

  private dezenas: Array<number> = new Array(6);
  private dezHabilitadas: boolean = true;

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.primDezena.nativeElement.focus();
    this.alternarSurpresinha(false);
  }

  gerarJogo() {
    let that = this;
    this.apiService.gerarNumeros().subscribe(
      (data: Array<number>) => {
        that.dezenas = data;
        this.alternarSurpresinha(true);
      }
    );
  }

  alternarSurpresinha(modoSurpresinha) {
    this.dezHabilitadas = !modoSurpresinha;
    this.btnCancelSurpresinha.nativeElement.style.display = modoSurpresinha ? 'inline-block' : 'none';
  }

  cancelarSurpresinha () {
    this.alternarSurpresinha(false);
    this.dezenas = new Array(6);
  }

  apostar() {
    if (this.validar()) {
      this.apiService.apostar(this.dezenas).subscribe(
        res => {
          this.exibirJogoRealizado(res);
          this.dezenas = new Array(6);
          this.primDezena.nativeElement.focus();

          this.alternarSurpresinha(false);
          console.log(res);
        },
        err => {
          alert(err.error.ExceptionMessage);
          console.log(err);
        }
      );
    }
  }

  validar () {
    for (let i = 0; i < this.dezenas.length; i++) {      
      let dez = this.dezenas[i];

      if (!dez) {
        alert('É necessário preencher as 6 Dezenas.');
        return false;
      }

      let item = Number(dez);

      if (isNaN(item)) {
        alert('Dezena inválida: ' + dez);
        return false;
      }

      this.dezenas[i] = item;
    }

    return true;
  }

  exibirJogoRealizado (aposta) {
    this.pJogoRealizado.nativeElement.innerHTML = 'Id: ' + aposta["Id"] + '<br />Número: ' + aposta["Numero"] + '<br />Data: ' + aposta["Data"];
  }
}
