import React, { useState } from 'react';

const Modal: React.FC<{
    isOpen: boolean;
    onClose: () => void;
    currentValue: number;
    onUpdateValue: (value: number) => void;
}> = ({ isOpen, onClose, currentValue, onUpdateValue }) => {
  const [newValue, setNewValue] = useState(currentValue);

  const handleChange = (e: { target: { value: string; }; }) => {
    setNewValue(parseFloat(e.target.value));
  };

  const handleSubmit = () => {
    onUpdateValue(newValue);
    onClose();
  };

  if (!isOpen) {
    return null;
  }

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-gray-800 bg-opacity-50">
      <div className="rounded-lg bg-white p-8 shadow-lg">
        <h2 className="mb-4 text-xl font-semibold">Change Value</h2>
        <input value={newValue} onChange={handleChange} className="mb-4 border p-2" />
        <div className="flex justify-end">
          <button
            onClick={onClose}
            className="mr-2 rounded bg-gray-300 px-4 py-2 hover:bg-gray-400"
          >
            Abbrechen
          </button>
          <button
            onClick={handleSubmit}
            className="rounded bg-blue-500 px-4 py-2 text-white hover:bg-blue-600"
          >
            Speichern
          </button>
        </div>
      </div>
    </div>
  );
};

export { Modal };
