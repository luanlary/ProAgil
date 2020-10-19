import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder,  FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../_modelos/Evento';
import { EventoService } from '../_servicos/evento.service';
import { defineLocale} from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { BsLocaleService} from 'ngx-bootstrap/datepicker';
import { templateJitUrl } from '@angular/compiler';
import { ToastrService } from 'ngx-toastr';

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  eventos: Evento[];
  evento: Evento;
  eventosFiltrados: Evento[];
  imagemLargura = 50;
  imagemMargem = 50;
  mostrarImagem = true;
  registerForm: FormGroup;
  modoSalvar = 'post';
  bodyDeletarEvento: string;
  // tslint:disable-next-line: variable-name
  _filtroLista: string;
  titulo = 'Eventos';
  file: File;
  fileNameToUpdate: string;
  dataAtual: string;


  constructor(
    private eventoService: EventoService,
    private modalservice: BsModalService,
    private fb: FormBuilder,
    private localeservice: BsLocaleService,
    private toastr: ToastrService,
    ) {
      this.localeservice.use('pt-br');
     }

ngOnInit(): void {
  this.validation();
  this.getEventos();
  }

  openModal(template: any): void{
    this.registerForm.reset();
    template.show();
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
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      imagemurl: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]

    });
  }

  uploadImagens(): void{

    if (this.modoSalvar === 'post')
    {
      const fileName = this.evento.imagemurl.split('\\', 3);
      this.evento.imagemurl = fileName[2];
      this.dataAtual = new Date().getDate().toString();

      this.eventoService.postUploads(this.file, fileName[2]).subscribe(
        () => {
          this.dataAtual = new Date().getMilliseconds().toString();
          this.getEventos();
        },
        e => {
          console.log(e.error);
        }
      );
    }else{
      this.evento.imagemurl = this.fileNameToUpdate;
      this.eventoService.postUploads(this.file, this.fileNameToUpdate).subscribe(
        () => {
          this.dataAtual = new Date().getMilliseconds().toString();
          this.getEventos();
        },
        e => {
          console.log(e.error);
        }
      );
    }
  }

  salvarAlteracao(template: any): void{
    if (this.registerForm.valid)
    {
      if (this.modoSalvar === 'post')
      {
        this.evento = Object.assign({}, this.registerForm.value);
        this.uploadImagens();
        this.eventoService.postEventos(this.evento)
        .subscribe(
          (novoevento: Evento) => {
            console.log(novoevento);
            template.hide();
            this.toastr.success('Evento inserido com Sucesso');
            this.getEventos();
          },
          e => {
            console.log(e.error);
            this.toastr.error(`Erro ao gravar registro: ${e.error}`);
          }
        );
      }else{
        this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);
        this.uploadImagens();
        this.eventoService.putEventos(this.evento)
        .subscribe(
          (novoevento: Evento) => {
            console.log(novoevento);
            template.hide();
            this.toastr.success('Evento atualizado com Sucesso');
            this.getEventos();
          },
          e => {
            console.log(e.error);
            this.toastr.error(`Erro ao gravar registro: ${e.error}`);
          }
        );
      }
    }
  }

  editarEvento(template: any, evento: Evento): void{  
    this.openModal(template);
    this.modoSalvar = 'put';
    this.evento = Object.assign({}, evento);
    this.fileNameToUpdate = evento.imagemurl.toString();
    this.evento.imagemurl = '';
    this.registerForm.patchValue(this.evento);
  }

  novoEvento(template: any)
  {
    this.openModal(template);
    this.modoSalvar = 'post';

  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.tema}`;
  }

  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
          template.hide();
          this.getEventos();
          this.toastr.success('Deletado com Sucesso');
        }, error => {
          console.log(error);
          this.toastr.error(`Erro ao tentar deletar: ${error}`);
        }
    );
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
        this.toastr.error(`Erro ao tentar carregar os eventos: ${e.error}`);
      }
    );
  }

  onFileChange(event): void{
    const reader = new FileReader();
    if (event.target.files && event.target.files.length)
    {
      this.file = event.target.files[0];
      console.log(this.file);

    }

  }

}
