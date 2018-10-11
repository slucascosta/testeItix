import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest, HttpEventType } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  API_URL = 'http://localhost:4300/MegaSena';

  constructor(private httpClient: HttpClient) { }

  obterApostas () {
    return this.httpClient.get(`${this.API_URL}/ObterApostas`)
  }

  gerarNumeros () {
    return this.httpClient.get(`${this.API_URL}/GerarNumero`);
  }

  apostar (numeros) {
    let httpHeaders = new HttpHeaders({
      'Content-Type' : 'application/x-www-form-urlencoded'
    }); 

    let options = {
      headers: httpHeaders
    }; 

    return this.httpClient.post(`${this.API_URL}/Apostar`, numeros);
  }

  sortear () {
    return this.httpClient.get(`${this.API_URL}/Sortear`);
  }

  obterGanhadoresQuina (id) {
    return this.httpClient.get(`${this.API_URL}/Sortear/Quina/` + id);
  }

  obterGanhadoresQuadra (id) {
    return this.httpClient.get(`${this.API_URL}/Sortear/Quadra/` + id);
  }
}
