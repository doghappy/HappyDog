import { environment } from '../../environments/environment';

export class BaseService {

  protected server: string = environment.server;
}
