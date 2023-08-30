import { AuctionItem } from '@/models/auction-item';
import { AuctionItemComponent } from './auction-item';

const AuctionListComponent: React.FC<{ items: AuctionItem[] }> = ({ items }) =>
  items.map((item) => <AuctionItemComponent key={item.id} item={item} />);

export { AuctionListComponent };
