import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  private apostas: Array<object> = [];

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.obterApostas(this);
  }

  obterApostas(that) {
    that.apiService.obterApostas().subscribe(
      (data: Array<object>) => {
        that.apostas = data;
        console.log(data);
      },
      error => console.log(error)
    );

    setTimeout(function () { that.obterApostas(that) }, 5000);
  }
}
