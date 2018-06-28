import { HttpBaseResult } from "./httpBaseResult";

export class HttpDataResult<T> extends HttpBaseResult {
  public data: T;
}
