# digitalPersona
Common methods about digitalPersona 4500 Reader

## Advice

This only works with `DPUruNet` version 1.0.0.0 be careful, also have to install DigitalPersona U.are.U SDK.

## Usage for Nuget

Just create the reference of the namespace and create another for the Reader from `DPUruNet`:

	PersonalUAU.DigitalPersona objMethods;
    Reader objReader;

Then initialize the reader with `GetDevice` method:
	
    objReader = objMethods.GetDevice();
   
If you are using Windows Forms you need call `CaptureFingerprint` method and the add the `Reader` reference also the PictureBox control reference:

	objMethods.CaptureFingerprint(objReader,pictureBoxReference);

If you are using WPF just add an `Image` control reference:

	objMethods.CaptureFingerprintWPF(objReader,imageReference);
    
To stop captures in Windows Forms call:

	objMethods.StopCaptureFingerprint();
    
To stop captures in WPF call:
	
    objMethods.StopCaptureFingerprintWPF();
    
To register the fingerprint use:

	objMethods.ShowWindowEnrollment(objReader);
    
This method works for both WPF and Forms.

and finally to get the xml of the fingerprint use:
	
    xml = objMethods.GetFingerprint_XML();
    
## Examples

There are examples for Forms and WPF inside in the project.

    


