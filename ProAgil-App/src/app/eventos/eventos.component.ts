import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
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
  modalRef: BsModalRef;
  registerForm: FormGroup;
  
  // tslint:disable-next-line: variable-name
  _filtroLista: string;

  constructor(
    private eventoService: EventoService,
    private modalservice: BsModalService
    ) { }

ngOnInit(): void {
  this.validation();
  this.getEventos();
  }

  openModal(template: TemplateRef<any>): void{
    this.modalRef = this.modalservice.show(template);
  }

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

  validation(): void{
    this.registerForm = new FormGroup({
      tema: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]),
      local: new FormControl('', Validators.required),
      dataEvento: new FormControl('', Validators.required),
      qtdPessoas: new FormControl('', [Validators.required, Validators.max(120000)]),
      imagemurl: new FormControl('', Validators.required),
      telefone: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email])

    });
  }

  salvarAlteracao(): void{

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
