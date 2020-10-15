import { Component, OnInit } from '@angular/core';
import { Evento } from '../_modelos/Evento';
import { EventoService } from '../_servicos/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  eventos: Evento[];
  eventosFiltrados: Evento[];
  imagemLargura = 50;
  imagemMargem = 50;
  mostrarImagem = true;
  // tslint:disable-next-line: variable-name
  _filtroLista: string;

  get filtroLista(): string
  {
    return this._filtroLista;
  }

  set filtroLista(value: string)
  {
    this._filtroLista = value;
    this.eventosFiltrados = this._filtroLista ? this.filtrarEvento(this._filtroLista) : this.eventos;
  }

  filtrarEvento(filtrarPor: string): Evento[]
  {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  constructor(private eventoService: EventoService) { }

  // tslint:disable-next-line: typedef
  ngOnInit() {
    this.getEventos();
   
    
  }

  getMostrarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }
  // tslint:disable-next-line: typedef
  getEventos(){
     this.eventoService.getAllEventos()
    .subscribe(
      // tslint:disable-next-line: variable-name
      (_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventosFiltrados = _eventos;
      },
      e => {
        console.log(e.error);
      }
    );
  }

}
