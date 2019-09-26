import { HttpBaseResult } from './http-base-result';

export class HttpDataResult<T> extends HttpBaseResult {
    data: T;
}
