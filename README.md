<p align="center">
 <h2 align="center">crypto-hash-verify</h2>
 <p align="center">Encrypt & Decrypt sensitive value with salt using <b>Password Key Derivation Function 2</b></p>
 <br/>
 <p align="center">
 <img src="https://img.shields.io/github/stars/chandru415/crypto-hash-verify?style=for-the-badge" />
 <img src="https://img.shields.io/github/watchers/chandru415/crypto-hash-verify?style=for-the-badge" />
  <a href="https://www.nuget.org/packages/CryptoHashVerify/">
   <img src="https://img.shields.io/nuget/dt/CryptoHashVerify?style=for-the-badge" />
 </a>
 </p>
</p>
<br/>

---

 **crypto-hash-verify** gives the ability to encrypt and decrypt sensitive data using the key derivation function(s). 

To install the *package* to any .Net Core application please click [here](https://www.nuget.org/packages/CryptoHashVerify/).

<h4> Use case: password encrypt & decrypt </h4>


* Create .Net core console application (C#)

* Under project dependencies - add CryptoHashVerify from the nuget package manager.

<img align="center" src="./assests/nugetpackagess.png" alt="nuget package image">

* ***GenerateHashString*** method will return a tuple consists 
  * hashed password
  * salt value

<img align="center" src="./assests/input-password.png" alt="input-password">

<br/>

*output*

<img align="center" src="./assests/hashedoutput.png" alt="hashedoutput">

<br/>


* ***VerifyHashString*** method will return a true password matches otherwise false.

<img align="center" src="./assests/foutput.PNG" alt="foutput">

<br/>

<div align="center">

### Show some ❤️ by starring some of the repositories!

</div>
