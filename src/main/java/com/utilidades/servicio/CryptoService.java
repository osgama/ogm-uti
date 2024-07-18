package com.utilidades.servicio;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import javax.crypto.spec.SecretKeySpec;
import javax.crypto.Cipher;
import java.util.Base64;

@Service
public class CryptoService {

    @Value("${llave.encriptacion}")
    private String AES_KEY;

    public String encrypt(String data) throws Exception {
        SecretKeySpec skeySpec = new SecretKeySpec(AES_KEY.getBytes("UTF-8"), "AES");
        Cipher cipher = Cipher.getInstance("AES/ECB/PKCS5PADDING");
        cipher.init(Cipher.ENCRYPT_MODE, skeySpec);
        byte[] encrypted = cipher.doFinal(data.getBytes());
        return Base64.getEncoder().encodeToString(encrypted);
    }

    public String decrypt(String encryptedData) throws Exception {
        SecretKeySpec skeySpec = new SecretKeySpec(AES_KEY.getBytes("UTF-8"), "AES");
        Cipher cipher = Cipher.getInstance("AES/ECB/PKCS5PADDING");
        cipher.init(Cipher.DECRYPT_MODE, skeySpec);
        byte[] original = cipher.doFinal(Base64.getDecoder().decode(encryptedData));
        return new String(original);
    }
}