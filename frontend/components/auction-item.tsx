import { AuctionItem } from "@/models/auction-item";

const AuctionItemComponent: React.FC<{ item: AuctionItem }> = ({ item }) => (
  <div className="rounded-lg bg-white p-6 shadow-md">
    <h2 className="mb-2 text-xl font-semibold">{item.model}</h2>
    <p className="mb-1 text-gray-600">Auction Number: {item.nummer}</p>
    <p className="mb-1 text-gray-600">Serial Number: {item.seriennummer}</p>
    <p className="text-blue-500">Price: {item.prices} €</p>
    <button className="mt-2 rounded bg-blue-500 px-4 py-2 text-white">Bieten</button>
  </div>
);

export { AuctionItemComponent };
