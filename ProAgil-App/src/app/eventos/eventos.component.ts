import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

 /* eventos: any = [
    {EventoId: 1, Tema: 'Angular 7', Local: 'Belo Horizonte', Lote: '4 Lote', QtdPessoas: 458},
    {EventoId: 2, Tema: 'Asp.Net Core', Local: 'São Paulo', Lote: '3 Lote', QtdPessoas: 125},
    {EventoId: 3, Tema: 'Fullstack', Local: 'Rio de Janeiro', Lote: '1 Lote', QtdPessoas: 585},
  ];*/

  eventos: any;

  constructor(private http: HttpClient) { }

  // tslint:disable-next-line: typedef
  ngOnInit() {
    this.getEventos();
  }
  // tslint:disable-next-line: typedef
  getEventos(){
     this.http.get('http://localhost:5000/api/values')
    .subscribe(
      response => {
        this.eventos = response;
      },
      e => {
        console.log(e.error);
      }
    );
  }

}
