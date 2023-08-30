import { AuctionItem } from "@/models/auction-item";
import { AuctionItemComponent } from "./auction-item";

const AuctionListComponent: React.FC<{ items: AuctionItem[] }> = ({ items }) => (
  <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4">
    {items.map((item) => (
      <AuctionItemComponent key={item.id} item={item} />
    ))}
  </div>
);

export { AuctionListComponent };
