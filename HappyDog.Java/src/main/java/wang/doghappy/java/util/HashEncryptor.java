package wang.doghappy.java.util;

import org.apache.commons.codec.digest.HmacAlgorithms;
import org.apache.commons.codec.digest.HmacUtils;

public class HashEncryptor {
    public static String hmacSha1(String key, String text) {
        var hmac = new HmacUtils(HmacAlgorithms.HMAC_SHA_1, key);
        return hmac.hmacHex(text);
    }
}
