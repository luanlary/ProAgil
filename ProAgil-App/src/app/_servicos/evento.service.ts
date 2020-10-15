import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../_modelos/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  private baseUrl = 'http://localhost:5000/api/evento'  ;

constructor(private http: HttpClient) { }


getAllEventos(): Observable<Evento[]>{
  return this.http.get<Evento[]>(this.baseUrl);
 
}
getEventosById(id: number): Observable<Evento>{
  return this.http.get<Evento>(`${this.baseUrl}/${id}`);
 
}
getEventosByTema(tema: string): Observable<Evento[]>{
  return this.http.get<Evento[]>(`${this.baseUrl}/getByTema/${tema}`);
 
}

}
