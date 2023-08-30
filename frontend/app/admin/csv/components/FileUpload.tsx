import React, { useState } from 'react';
import { parse } from 'papaparse';

interface FileUploadProps {
  onUpload: (data: any[]) => void;
}

const FileUpload: React.FC<FileUploadProps> = ({ onUpload }) => {
  const [selectedFile, setSelectedFile] = useState<File | null>(null);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    setSelectedFile(file || null);
  };

  const handleUpload = () => {
    if (selectedFile) {
      parse(selectedFile, {
        header: true,
        transformHeader: (header, index) => {
          const mapHeader: {
            [key: string]: string;
          } = {
            Nummer: 'nummer',
            Model: 'model',
            Seriennummer: 'seriennummer',
            'Aktueller Preis': 'price',
          };
    
          return mapHeader[header] ?? header;
        },
        complete: (result) => {
          if (result.data && result.data.length > 0) {
            onUpload(result.data);
          }
        }
      });
    }
  };

  return (
    <div>
      <label>
        CSV:
        <input type="file" accept=".csv" onChange={handleFileChange} />
      </label>
      <button className="mt-2 rounded bg-blue-500 px-4 py-2" onClick={handleUpload}>
        Upload CSV
      </button>
    </div>
  );
};

export default FileUpload;
