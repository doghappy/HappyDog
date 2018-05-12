import { environment } from '../../environments/environment';

export class BaseService {

  protected server: string = environment.server;

  protected reqOptions = {
    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
  }
}
