import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../_modelos/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  private baseUrl = 'http://localhost:5000/api/evento';
  

constructor(private http: HttpClient) { 
   
}


getAllEventos(): Observable<Evento[]>{
  
  return this.http.get<Evento[]>(this.baseUrl);
}
getEventosById(id: number): Observable<Evento>{
  return this.http.get<Evento>(`${this.baseUrl}/${id}`);
}
getEventosByTema(tema: string): Observable<Evento[]>{
  return this.http.get<Evento[]>(`${this.baseUrl}/getByTema/${tema}`);
}

postEventos(evento: Evento) {
  return this.http.post<Evento>(this.baseUrl, evento);
}

putEventos(evento: Evento) {
  return this.http.put<Evento>(`${this.baseUrl}/${evento.id}`, evento);
}

deleteEvento(id: number) {
  return this.http.delete(`${this.baseUrl}/${id}`);
}


postUploads(file: File, filename: string) {
  const formdata = new FormData();
  formdata.append('file', file, filename);
  return this.http.post(`${this.baseUrl}/upload`, formdata);
}


}
