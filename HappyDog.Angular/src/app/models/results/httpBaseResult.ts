import { CodeResult } from "./codeResult";
import { NotifyResult } from "./notifyResult";

export class HttpBaseResult {
  public code: CodeResult;
  public notify: NotifyResult;
  public message: string;
}
